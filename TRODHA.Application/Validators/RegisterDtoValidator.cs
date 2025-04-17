using FluentValidation;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boþ olamaz")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz")
                .MaximumLength(100).WithMessage("E-posta adresi 100 karakterden uzun olamaz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Þifre boþ olamaz")
                .MinimumLength(8).WithMessage("Þifre en az 8 karakter uzunluðunda olmalýdýr")
                .Matches("[A-Z]").WithMessage("Þifre en az 1 büyük harf içermelidir")
                .Matches("[a-z]").WithMessage("Þifre en az 1 küçük harf içermelidir")
                .Matches("[0-9]").WithMessage("Þifre en az 1 rakam içermelidir");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad alaný boþ olamaz")
                .MaximumLength(50).WithMessage("Ad 50 karakterden uzun olamaz");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad alaný boþ olamaz")
                .MaximumLength(50).WithMessage("Soyad 50 karakterden uzun olamaz");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Doðum tarihi boþ olamaz")
                .LessThan(DateTime.Now.AddYears(-10)).WithMessage("En az 10 yaþýnda olmalýsýnýz")
                .GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Doðum tarihi 100 yýldan eski olamaz");
        }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boþ olamaz")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Þifre boþ olamaz");
        }
    }
}
