using DFT_MVC.Data;
using DFT_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DFT_MVC.Services
{
	public class SubcategoryService : ImageService, ISubcategoryService
	{
        private const int ImageBigWidth = 480;
        private const int ImageSmallWidth = 150;

        private readonly string tableName = "Subcategories";
        private readonly string imageBig = "ImageBig";
        private readonly string imageSmall = "ImageSmall";

        private readonly DFT_MVC_Context _dbContext;
		public int CategoryId { get; set; }

		public SubcategoryService(DFT_MVC_Context dbContext, IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory, dbContext)
        {
			_dbContext = dbContext;
		}
		public Task<Stream> GetImageBig(string id) => GetImageData(id, imageBig, tableName, _dbContext);
        public Task<Stream> GetImageSmall(string id) => GetImageData(id, imageSmall, tableName, _dbContext);

		public async Task CreateSubcategory(Subcategory subcategory, IFormFile? image)
		{
            if (image != null)
            {
                using var imgResult = await GetImageInputResult(image);

                var original = await SaveImage(imgResult, imgResult.Width);
                var ImageBig = await SaveImage(imgResult, ImageBigWidth);
                var ImageSmall = await SaveImage(imgResult, ImageSmallWidth);

                _dbContext.Subcategories.Add(new Subcategory
                {
                    Name = subcategory.Name,
                    Description = subcategory.Description,
                    CategoryId = subcategory.CategoryId,
                    ImageOriginal = original,
                    ImageBig = ImageBig,
                    ImageSmall = ImageSmall,
                });
            }
            else
            {
                _dbContext.Subcategories.Add(new Subcategory
                {
                    Name = subcategory.Name,
                    Description = subcategory.Description,
                    CategoryId = subcategory.CategoryId,
                });
            }
            await _dbContext.SaveChangesAsync();
        }
		public async Task UpdateSubcategory(Subcategory subcategory, IFormFile? image)
		{
            if (image != null)
            {
                using var imgResult = await GetImageInputResult(image);

                var original = await SaveImage(imgResult, imgResult.Width);
                var ImageBig = await SaveImage(imgResult, ImageBigWidth);
                var ImageSmall = await SaveImage(imgResult, ImageSmallWidth);

                subcategory.ImageOriginal = original;
                subcategory.ImageBig = ImageBig;
                subcategory.ImageSmall = ImageSmall;
            }

            _dbContext.Update(subcategory);
            await _dbContext.SaveChangesAsync();
        }

		public async Task<int> GetCategoryInfo(int id)
		{
			var category = await _dbContext.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
			return category!.Id;
		}


    }
}
