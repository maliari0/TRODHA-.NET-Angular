using FluentValidation;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Validators
{
    public class CreateGoalDtoValidator : AbstractValidator<CreateGoalDto>
    {
        public CreateGoalDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Hedef ba�l��� bo� olamaz")
                .MaximumLength(100).WithMessage("Ba�l�k 100 karakterden uzun olamaz");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("A��klama 500 karakterden uzun olamaz")
                .When(x => x.Description != null);

            RuleFor(x => x.Frequency)
                .InclusiveBetween(1, 6).WithMessage("S�kl�k 1 ile 6 aras�nda olmal�d�r");

            RuleFor(x => x.PeriodUnit)
                .NotEmpty().WithMessage("Periyot birimi bo� olamaz")
                .Must(periodUnit => new[] { "g�nde", "haftada", "ayda", "y�lda" }.Contains(periodUnit))
                .WithMessage("Ge�ersiz periyot birimi. 'g�nde', 'haftada', 'ayda' veya 'y�lda' olmal�d�r");

            RuleFor(x => x.ImportanceLevelId)
                .InclusiveBetween(1, 3).WithMessage("�nem seviyesi 1 ile 3 aras�nda olmal�d�r");
        }
    }

    public class UpdateGoalDtoValidator : AbstractValidator<UpdateGoalDto>
    {
        public UpdateGoalDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Hedef ba�l��� bo� olamaz")
                .MaximumLength(100).WithMessage("Ba�l�k 100 karakterden uzun olamaz");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("A��klama 500 karakterden uzun olamaz")
                .When(x => x.Description != null);

            RuleFor(x => x.Frequency)
                .InclusiveBetween(1, 6).WithMessage("S�kl�k 1 ile 6 aras�nda olmal�d�r");

            RuleFor(x => x.PeriodUnit)
                .NotEmpty().WithMessage("Periyot birimi bo� olamaz")
                .Must(periodUnit => new[] { "g�nde", "haftada", "ayda", "y�lda" }.Contains(periodUnit))
                .WithMessage("Ge�ersiz periyot birimi. 'g�nde', 'haftada', 'ayda' veya 'y�lda' olmal�d�r");

            RuleFor(x => x.ImportanceLevelId)
                .InclusiveBetween(1, 3).WithMessage("�nem seviyesi 1 ile 3 aras�nda olmal�d�r");
        }
    }
}
