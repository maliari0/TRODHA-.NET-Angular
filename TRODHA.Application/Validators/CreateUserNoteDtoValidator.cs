using FluentValidation;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Validators
{
    public class CreateUserNoteDtoValidator : AbstractValidator<CreateUserNoteDto>
    {
        public CreateUserNoteDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Not i�eri�i bo� olamaz")
                .MaximumLength(2000).WithMessage("Not i�eri�i 2000 karakterden uzun olamaz");
        }
    }

    public class UpdateUserNoteDtoValidator : AbstractValidator<UpdateUserNoteDto>
    {
        public UpdateUserNoteDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Not i�eri�i bo� olamaz")
                .MaximumLength(2000).WithMessage("Not i�eri�i 2000 karakterden uzun olamaz");
        }
    }
}
