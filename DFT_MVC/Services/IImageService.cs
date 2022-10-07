namespace DFT_MVC.Services
{
    using DFT_MVC.Models;

    public interface IImageService
    {
        Task Process(IEnumerable<ImageInput> images, int? categoryId = null, int? subcategoryId = null);
        Task<List<string>> GetAllImages();
        Task<Stream> GetFullscreen(string id);
        Task<Stream> GetThumbnailBig(string id);
        Task<Stream> GetThumbnailSmall(string id);
    }
}
