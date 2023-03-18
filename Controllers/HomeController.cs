using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Diagnostics;
using System.Text.Json;
using UploadYourFile.Models;
using UploadYourFile.Services;

namespace UploadYourFile.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlobStorageService _blobStorage;

        public HomeController(IBlobStorageService blobStorage)
        {
            _blobStorage = blobStorage;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(FormData model)
        {
            if (ModelState.IsValid)
            {
                // File upload
                await _blobStorage.UploadBlobFileAsync(model.FormFile!);

                // Get the Azure Function URL
                var functionUrl = "https://emailnotificationapplication.azurewebsites.net/api/EmailNotificationFunc?code=WdatGitLdy8FVaKO38i6k7COHOZILFKOju-xvwnPmwczAzFuB_hF-Q==";

                // Create an object that contains data to pass to an Azure Function
                var data = new
                {
                    email = model.Email,
                    message = $"The file {model.FormFile?.FileName} was successfully uploaded to the BLOB storage container. The file size is {model.FormFile?.Length} bytes."
                };

                // Converting an object into a JSON string
                var json = JsonSerializer.Serialize(data);

                // Create an object that contains the query parameters
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // Making an HTTP request to an Azure Function
                var httpClient = new HttpClient();
                var response = await httpClient.PostAsync(functionUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    // Error handling
                    return BadRequest();
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}