using FluentValidation;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Validators
{
    public class CreateGoalDtoValidator : AbstractValidator<CreateGoalDto>
    {
        public CreateGoalDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Hedef baþlýðý boþ olamaz")
                .MaximumLength(100).WithMessage("Baþlýk 100 karakterden uzun olamaz");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açýklama 500 karakterden uzun olamaz")
                .When(x => x.Description != null);

            RuleFor(x => x.Frequency)
                .InclusiveBetween(1, 6).WithMessage("Sýklýk 1 ile 6 arasýnda olmalýdýr");

            RuleFor(x => x.PeriodUnit)
                .NotEmpty().WithMessage("Periyot birimi boþ olamaz")
                .Must(periodUnit => new[] { "günde", "haftada", "ayda", "yýlda" }.Contains(periodUnit))
                .WithMessage("Geçersiz periyot birimi. 'günde', 'haftada', 'ayda' veya 'yýlda' olmalýdýr");

            RuleFor(x => x.ImportanceLevelId)
                .InclusiveBetween(1, 3).WithMessage("Önem seviyesi 1 ile 3 arasýnda olmalýdýr");
        }
    }

    public class UpdateGoalDtoValidator : AbstractValidator<UpdateGoalDto>
    {
        public UpdateGoalDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Hedef baþlýðý boþ olamaz")
                .MaximumLength(100).WithMessage("Baþlýk 100 karakterden uzun olamaz");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açýklama 500 karakterden uzun olamaz")
                .When(x => x.Description != null);

            RuleFor(x => x.Frequency)
                .InclusiveBetween(1, 6).WithMessage("Sýklýk 1 ile 6 arasýnda olmalýdýr");

            RuleFor(x => x.PeriodUnit)
                .NotEmpty().WithMessage("Periyot birimi boþ olamaz")
                .Must(periodUnit => new[] { "günde", "haftada", "ayda", "yýlda" }.Contains(periodUnit))
                .WithMessage("Geçersiz periyot birimi. 'günde', 'haftada', 'ayda' veya 'yýlda' olmalýdýr");

            RuleFor(x => x.ImportanceLevelId)
                .InclusiveBetween(1, 3).WithMessage("Önem seviyesi 1 ile 3 arasýnda olmalýdýr");
        }
    }
}
