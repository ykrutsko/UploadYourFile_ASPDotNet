using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics;
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
                await _blobStorage.UploadBlobFileAsync(model.FormFile!);
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