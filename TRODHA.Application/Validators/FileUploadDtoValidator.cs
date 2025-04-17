using FluentValidation;
using Microsoft.AspNetCore.Http;
using TRODHA.Application.DTOs;

namespace TRODHA.Application.Validators
{
    public class FileUploadDtoValidator : AbstractValidator<FileUploadDto>
    {
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };
        private readonly long _maxFileSize = 5 * 1024 * 1024; // 5MB

        public FileUploadDtoValidator()
        {
            RuleFor(x => x.File)
                .NotNull().WithMessage("Dosya seçilmedi")
                .Must(BeValidFileSize).WithMessage($"Dosya boyutu 5MB'ý geçmemelidir")
                .Must(BeValidFileType).WithMessage("Desteklenen dosya formatlarý: jpg, jpeg, png, gif, bmp, tiff");
        }

        private bool BeValidFileSize(IFormFile file)
        {
            if (file == null)
                return true;
                
            return file.Length <= _maxFileSize;
        }

        private bool BeValidFileType(IFormFile file)
        {
            if (file == null)
                return true;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return _allowedExtensions.Contains(extension);
        }
    }
}
