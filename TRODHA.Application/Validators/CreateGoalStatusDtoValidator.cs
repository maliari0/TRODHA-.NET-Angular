using FluentValidation;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Validators
{
    public class CreateGoalStatusDtoValidator : AbstractValidator<CreateGoalStatusDto>
    {
        public CreateGoalStatusDtoValidator()
        {
            RuleFor(x => x.GoalId)
                .GreaterThan(0).WithMessage("Geçerli bir hedef ID'si giriniz");

            RuleFor(x => x.RecordDate)
                .NotEmpty().WithMessage("Kayýt tarihi boþ olamaz")
                .LessThanOrEqualTo(DateTime.Now.Date).WithMessage("Gelecek tarihli kayýt eklenemez");

            RuleFor(x => x.RecordTime)
                .NotEmpty().WithMessage("Kayýt saati boþ olamaz");

            RuleFor(x => x.Duration)
                .GreaterThanOrEqualTo(0).WithMessage("Süre 0 veya daha büyük olmalýdýr")
                .When(x => x.Duration.HasValue);

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notlar 500 karakterden uzun olamaz")
                .When(x => !string.IsNullOrEmpty(x.Notes));
        }
    }
}
