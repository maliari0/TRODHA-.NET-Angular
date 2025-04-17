using FluentValidation;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Validators
{
    public class CreateUserNoteDtoValidator : AbstractValidator<CreateUserNoteDto>
    {
        public CreateUserNoteDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Not içeriði boþ olamaz")
                .MaximumLength(2000).WithMessage("Not içeriði 2000 karakterden uzun olamaz");
        }
    }

    public class UpdateUserNoteDtoValidator : AbstractValidator<UpdateUserNoteDto>
    {
        public UpdateUserNoteDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Not içeriði boþ olamaz")
                .MaximumLength(2000).WithMessage("Not içeriði 2000 karakterden uzun olamaz");
        }
    }
}
