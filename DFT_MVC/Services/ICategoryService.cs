using DFT_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DFT_MVC.Services
{
    public interface ICategoryService
    {
        Task CreateCategory(Category category, IFormFile? image);
        Task UpdateCategory(int categoryId, IFormFile? image);
        Task<Stream> GetImageSmall(string id);
        Task<Stream> GetImageBig(string id);
    }
}
