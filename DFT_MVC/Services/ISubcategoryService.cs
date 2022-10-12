using DFT_MVC.Models;

namespace DFT_MVC.Services
{
	public interface ISubcategoryService
	{
        Task CreateSubcategory(Subcategory subcategory, IFormFile? image);
        Task UpdateSubcategory(Subcategory subcategory, IFormFile? image);
        Task<int> GetCategoryInfo(int id);
        Task<Stream> GetImageSmall(string id);
        Task<Stream> GetImageBig(string id);
    }
}
