using DFT_MVC.Models;

namespace DFT_MVC.Services
{
    public interface IImageService
    {
        Task Process(IEnumerable<ImageInput> images);

        Task<List<string>> GetAllImages();
        Task<Stream> GetFullscreen(string id);
        Task<Stream> GetThumbnailBig(string id);
        Task<Stream> GetThumbnailSmall(string id);
    }
}
