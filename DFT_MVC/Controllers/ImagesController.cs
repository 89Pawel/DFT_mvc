using DFT_MVC.Models;
using DFT_MVC.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace DFT_MVC.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public IActionResult Upload() => View();

        [HttpPost]
        [RequestSizeLimit(100 * 1024 * 1024)]
        public async Task<IActionResult> Upload(IFormFile[]? images)
        {
            if (images != null)
            {
                if (images.Length > 10)
                {
                    ModelState.AddModelError("images", "Nie możesz dodać więcej jak 10 zdjęć!");
                    return View();
                }
                else
                {
                    await _imageService.Process(images.Select(i => new ImageInput
                    {
                        Name = i.FileName,
                        Type = i.ContentType,
                        Content = i.OpenReadStream()
                    }));

                    return RedirectToAction(nameof(this.Done));
                }
            }

            ModelState.AddModelError("images", "za dużo! maks 100mb");
            return RedirectToAction();

        }

        public IActionResult Done() => View();

    }
}
