namespace DFT_MVC.Services
{
    using DFT_MVC.Data;
    using DFT_MVC.Models;

    public interface IDisplayFromDBService
	{
        public Task<IEnumerable<Category>> GetCategories();
        public Task<IEnumerable<ImageData>> GetImages();
        public Task<ImageData> GetOneImage(int id);
        public Task GetData();

        public Task<Dictionary<Category, ImageData>> GetDataDict();
    }
}
