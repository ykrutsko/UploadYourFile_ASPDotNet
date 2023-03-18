using UploadYourFile.Models;

namespace UploadYourFile.Services
{
    public interface IBlobStorageService
    {
        // This method uploads a file submitted with the request
        Task UploadBlobFileAsync(IFormFile files);
    }
}
