using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRODHA.Application.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class AuthResponseDto
    {
        public UserDto User { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
