namespace DFT_MVC.Controllers
{
    using DFT_MVC.Data;
    using DFT_MVC.Models;
    using DFT_MVC.Services;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Serialization;

    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;
        private readonly DFT_MVC_Context _context;
        public ImagesController(IImageService imageService, DFT_MVC_Context context)
        {
            _imageService = imageService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Upload() => View();

        [HttpPost]
        [RequestSizeLimit(100 * 1024 * 1024)]
        public async Task<IActionResult> Upload(IFormFile[] images, int? categoryId = null, int? subcategoryId = null)
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
                            Content = i.OpenReadStream(),
                        }));

                        return RedirectToAction(nameof(this.Done));
                    }
                }
                else
                {
                    ModelState.AddModelError("images", "za dużo! maks 100mb"); 
                }
            

            return View();

        }

        public async Task<IActionResult> All() => View(await _imageService.GetAllImages());

        public async Task<IActionResult> Fullscreen(string id) => File(await _imageService.GetFullscreen(id), "image/jpeg");
        public async Task<IActionResult> ThumbnailBig(string id) => File(await _imageService.GetThumbnailBig(id), "image/jpeg");
        public async Task<IActionResult> ThumbnailSmall(string id) => File(await _imageService.GetThumbnailSmall(id), "image/jpeg");


        public IActionResult Done() => View();

    }
}
