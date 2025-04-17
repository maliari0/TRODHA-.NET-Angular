using FluentValidation;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi bo� olamaz")
                .EmailAddress().WithMessage("Ge�erli bir e-posta adresi giriniz")
                .MaximumLength(100).WithMessage("E-posta adresi 100 karakterden uzun olamaz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("�ifre bo� olamaz")
                .MinimumLength(8).WithMessage("�ifre en az 8 karakter uzunlu�unda olmal�d�r")
                .Matches("[A-Z]").WithMessage("�ifre en az 1 b�y�k harf i�ermelidir")
                .Matches("[a-z]").WithMessage("�ifre en az 1 k���k harf i�ermelidir")
                .Matches("[0-9]").WithMessage("�ifre en az 1 rakam i�ermelidir");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad alan� bo� olamaz")
                .MaximumLength(50).WithMessage("Ad 50 karakterden uzun olamaz");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad alan� bo� olamaz")
                .MaximumLength(50).WithMessage("Soyad 50 karakterden uzun olamaz");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Do�um tarihi bo� olamaz")
                .LessThan(DateTime.Now.AddYears(-10)).WithMessage("En az 10 ya��nda olmal�s�n�z")
                .GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Do�um tarihi 100 y�ldan eski olamaz");
        }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi bo� olamaz")
                .EmailAddress().WithMessage("Ge�erli bir e-posta adresi giriniz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("�ifre bo� olamaz");
        }
    }
}
