namespace UploadYourFile.Services
{
    public interface IBlobStorageService
    {
        Task UploadBlobFileAsync(IFormFile files);
    }
}
