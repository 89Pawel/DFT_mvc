using DFT_MVC.Data;
using DFT_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;

namespace DFT_MVC.Services
{
    public class CategoryService : ImageService, ICategoryService
    {
        private const int ImageBigWidth = 640;
        private const int ImageSmallWidth = 150;

        private readonly string tableName = "Categories";
        private readonly string imageBig = "ImageBig";
        private readonly string imageSmall = "ImageSmall";

        private readonly DFT_MVC_Context _dbContext;

        public CategoryService(IServiceScopeFactory serviceScopeFactory, DFT_MVC_Context dbContext) : base(serviceScopeFactory, dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Stream> GetImageSmall(string id) => GetImageData(id, imageSmall, tableName, _dbContext);
        public Task<Stream> GetImageBig(string id) => GetImageData(id, imageBig, tableName, _dbContext);

        public async Task CreateCategory(Category category, IFormFile? image)
        {
            if (image != null)
            {
                using var imgResult = await GetImageInputResult(image);

                var original = await SaveImage(imgResult, imgResult.Width);
                var ImageBig = await SaveImage(imgResult, ImageBigWidth);
                var ImageSmall = await SaveImage(imgResult, ImageSmallWidth);

                _dbContext.Categories.Add(new Category
                {
                    Name = category.Name,
                    ImageOriginal = original,
                    ImageBig = ImageBig,
                    ImageSmall = ImageSmall,
                });
            }
            else
            {
                _dbContext.Categories.Add(category);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category inputCategory, IFormFile? image)
        {
            var category = _dbContext.Categories.Single(i => i.Id == inputCategory.Id);

            category.Name = inputCategory.Name;

            if (image != null)
            {
                using var imgResult = await GetImageInputResult(image);

                var original = await SaveImage(imgResult, imgResult.Width);
                var ImageBig = await SaveImage(imgResult, ImageBigWidth);
                var ImageSmall = await SaveImage(imgResult, ImageSmallWidth);

                category.ImageOriginal = original;
                category.ImageBig = ImageBig;
                category.ImageSmall = ImageSmall; 
            }

            _dbContext.Update(category);
            await _dbContext.SaveChangesAsync();
        }

    }
}
