using System.Threading.Tasks;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<bool> ForgotPasswordAsync(string email);
    }
}
