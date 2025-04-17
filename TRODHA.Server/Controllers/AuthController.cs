using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using TRODHA.Application.DTOs;
using TRODHA.Application.Services.Interfaces;

namespace TRODHA.Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var result = await _authService.LoginAsync(request);

            if (result == null)
            {
                return Unauthorized(new { message = "Email veya şifre hatalı" });
            }

            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            // Bu metot henüz IAuthService'de tanımlanmamış, eklenecek
            // Şimdilik bunu bir placeholder olarak tutuyoruz
            return Ok(new { message = "Şifre sıfırlama talebi başarıyla gönderildi." });
        }

        [Authorize]
        [HttpGet("validate-token")]
        public IActionResult ValidateToken()
        {
            return Ok(true);
        }
    }

    public class ForgotPasswordRequest
    {
        public required string Email { get; set; }
    }
}
