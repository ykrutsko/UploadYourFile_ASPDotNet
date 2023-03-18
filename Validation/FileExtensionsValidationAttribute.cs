using System.ComponentModel.DataAnnotations;

namespace UploadYourFile.Validation
{
    public class FileExtensionsValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var file = (IFormFile)value;
                string extention = Path.GetExtension(file.FileName);

                if (extention != ".docx")
                {
                    return new ValidationResult(".docx only");
                }
            }

            return ValidationResult.Success!;
        }
    }
}
