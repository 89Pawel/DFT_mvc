using DFT_MVC.Models;

namespace DFT_MVC.Services
{
    public interface IImageService
    {
        public Task Process(IEnumerable<ImageInput> images);
    }
}
