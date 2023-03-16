using System.ComponentModel.DataAnnotations;
using UploadYourFile.Validation;

namespace UploadYourFile.Models
{
    public class FormData
    {
        [Required(ErrorMessage = "Email not specified")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "File not specified")]
        [FileExtensionsValidation]
        public IFormFile? FormFile { get; set; }
    }
}
