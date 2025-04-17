using FluentValidation;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Validators
{
    public class CreateRecommendationDtoValidator : AbstractValidator<CreateRecommendationDto>
    {
        public CreateRecommendationDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("�neri i�eri�i bo� olamaz")
                .MaximumLength(1000).WithMessage("�neri i�eri�i 1000 karakterden uzun olamaz");
        }
    }

    public class UpdateRecommendationDtoValidator : AbstractValidator<UpdateRecommendationDto>
    {
        public UpdateRecommendationDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("�neri i�eri�i bo� olamaz")
                .MaximumLength(1000).WithMessage("�neri i�eri�i 1000 karakterden uzun olamaz");
        }
    }
}
