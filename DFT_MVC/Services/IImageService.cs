namespace DFT_MVC.Services
{
    using DFT_MVC.Models;
    using SixLabors.ImageSharp;

    public interface IImageService
    {
        Task Process(IEnumerable<ImageInput> images);
        Task<Image> GetImageInputResult(IFormFile? image);
        Task<List<string>> GetAllImages();
        Task<Stream> GetFullscreen(string id);
        Task<Stream> GetThumbnailBig(string id);
        Task<Stream> GetThumbnailSmall(string id);
    }
}
