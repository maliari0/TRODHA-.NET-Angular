using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TRODHA.Application.DTOs;
using TRODHA.Application.Services.Interfaces;
using TRODHA.Core.Interfaces;
using TRODHA.Core.Models;

namespace TRODHA.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                // Gelen veriyi kontrol et
                if (registerDto == null)
                    throw new ArgumentNullException(nameof(registerDto));

                if (string.IsNullOrEmpty(registerDto.Email) ||
                    string.IsNullOrEmpty(registerDto.Password) ||
                    string.IsNullOrEmpty(registerDto.FirstName) ||
                    string.IsNullOrEmpty(registerDto.LastName))
                    throw new ApplicationException("All required fields must be filled");

                // Email var mı kontrol et
                if (await _userRepository.EmailExistsAsync(registerDto.Email))
                    throw new ApplicationException("Email already exists");

                if (!IsPasswordValid(registerDto.Password))
                    throw new ApplicationException("Password must be at least 8 characters and include uppercase, lowercase, and numbers");

                // Create password hash
                CreatePasswordHash(registerDto.Password, out string passwordHash, out string passwordSalt);

                var user = new User
                {
                    Email = registerDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    BirthDate = registerDto.BirthDate,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Debug için
                Console.WriteLine($"Trying to add user: {user.Email}, FirstName: {user.FirstName}, LastName: {user.LastName}");

                await _userRepository.AddAsync(user);

                var token = GenerateJwtToken(user);

                return new AuthResponseDto
                {
                    User = new UserDto
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        BirthDate = user.BirthDate
                    },
                    Token = token
                };
            }
            catch (Exception ex)
            {
                // Hata ayrıntılarını yakala
                string innerMessage = ex.InnerException?.Message ?? "No inner exception";
                throw new ApplicationException($"Registration failed: {ex.Message}. Inner details: {innerMessage}", ex);
            }
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(loginDto.Email);
                if (user == null)
                    return null;

                if (!VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
                    return null;

                // Update last login time
                user.LastLoginAt = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);

                var token = GenerateJwtToken(user);

                return new AuthResponseDto
                {
                    User = new UserDto
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        BirthDate = user.BirthDate
                    },
                    Token = token
                };
            }
            catch (Exception ex)
            {
                string innerMessage = ex.InnerException?.Message ?? "No inner exception";
                throw new ApplicationException($"Login failed: {ex.Message}. Inner details: {innerMessage}", ex);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret not configured"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            using (var hmac = new System.Security.Cryptography.HMACSHA512(saltBytes))
            {
                var computedHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
                return computedHash == storedHash;
            }
        }

        private bool IsPasswordValid(string password)
        {
            // Password must be at least 8 characters and contain at least one uppercase letter, one lowercase letter, and one number
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit);
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return false;

            // Burada normalde şifre sıfırlama mantığı yer alacak
            // Örneğin: 
            // 1. Sıfırlama token'ı oluşturma
            // 2. Email gönderme işlemi
            // 3. Token'ı veritabanına kaydetme

            return true;
        }
    }
}
