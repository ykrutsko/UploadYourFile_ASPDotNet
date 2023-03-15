using System.ComponentModel.DataAnnotations;

namespace UploadYourFile.Models
{
    public class Validation
    {
        [Required(ErrorMessage = "Email not specified")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "File not specified")]
        [FileExtensions(Extensions = "docx", ErrorMessage = "File must be .docx")]
        public string? FileName { get; set; }
    }
}
