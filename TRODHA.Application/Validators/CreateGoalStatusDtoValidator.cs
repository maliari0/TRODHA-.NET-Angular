using FluentValidation;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Validators
{
    public class CreateGoalStatusDtoValidator : AbstractValidator<CreateGoalStatusDto>
    {
        public CreateGoalStatusDtoValidator()
        {
            RuleFor(x => x.GoalId)
                .GreaterThan(0).WithMessage("Ge�erli bir hedef ID'si giriniz");

            RuleFor(x => x.RecordDate)
                .NotEmpty().WithMessage("Kay�t tarihi bo� olamaz")
                .LessThanOrEqualTo(DateTime.Now.Date).WithMessage("Gelecek tarihli kay�t eklenemez");

            RuleFor(x => x.RecordTime)
                .NotEmpty().WithMessage("Kay�t saati bo� olamaz");

            RuleFor(x => x.Duration)
                .GreaterThanOrEqualTo(0).WithMessage("S�re 0 veya daha b�y�k olmal�d�r")
                .When(x => x.Duration.HasValue);

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notlar 500 karakterden uzun olamaz")
                .When(x => !string.IsNullOrEmpty(x.Notes));
        }
    }
}
