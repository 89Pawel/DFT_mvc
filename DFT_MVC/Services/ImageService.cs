namespace DFT_MVC.Services
{
    using DFT_MVC.Data;
    using DFT_MVC.Models;
    using Microsoft.Build.Framework;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.Processing;
    using System.Collections.Generic;

    public class ImageService : IImageService
    {
        private const int ThumbnailBigWidth = 480;
        private const int ThumbnailSmallWidth = 150;
        private const int FullscreenWidth = 1024;

        private readonly DFT_MVC_Context dbContext;
        private readonly IServiceScopeFactory serviceScopeFactory;

        private readonly string fullscreen = "FullscreenContent";
        private readonly string thumbnailBig = "ThumbnailBigContent";
        private readonly string thumbnailSmall = "ThumbnailSmallContent";


        public ImageService(IServiceScopeFactory serviceScopeFactory, DFT_MVC_Context dbContext)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.dbContext = dbContext;
        }

        public Task<List<string>> GetAllImages() => dbContext.ImageData.Select(i => i.Id.ToString()).ToListAsync();

        public Task<Stream> GetFullscreen(string id) => GetImageData(id, fullscreen);

        public Task<Stream> GetThumbnailBig(string id) => GetImageData(id, thumbnailBig);

        public Task<Stream> GetThumbnailSmall(string id) => GetImageData(id, thumbnailSmall);

        public async Task Process(IEnumerable<ImageInput> images, int id)
        {
            var tasks = images
                .Select(image => Task.Run(async () =>
                {
                    using var imgResult = await Image.LoadAsync(image.Content);

                    var original = await SaveImage(imgResult, imgResult.Width);
                    var fullscreen = await SaveImage(imgResult, FullscreenWidth);
                    var thumbnailBig = await SaveImage(imgResult, ThumbnailBigWidth);
                    var thumbnailSmall = await SaveImage(imgResult, ThumbnailSmallWidth);

                    var database = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DFT_MVC_Context>();

                    //database.ImageData.Add(new ImageData
                    database.ImageData.Add(new ImageData
                    {
                        OriginalFileName = image.Name,
                        OriginalType = image.Type,
                        OriginalContent = original,
                        FullscreenContent = fullscreen,
                        ThumbnailBigContent = thumbnailBig,
                        ThumbnailSmallContent = thumbnailSmall,
                        CategoryId = id
                    });
                    await database.SaveChangesAsync();

                })).ToList();

            await Task.WhenAll(tasks);
        }

        private async Task<byte[]> SaveImage(Image image, int resizeWidth)
        {
            var width = image.Width;
            var height = image.Height;

            if (width > resizeWidth)
            {
                height = (int)((double)resizeWidth / width * height);
                width = resizeWidth;
            }

            image.Mutate(i => i
                    .Resize(new Size(width, height)));

            image.Metadata.ExifProfile = null;

            var memoryStream = new MemoryStream();

            await image.SaveAsJpegAsync(memoryStream, new JpegEncoder
            {
                Quality = 75
            });

            return memoryStream.ToArray();
        }
         private async Task<Stream> GetImageData(string id, string size)
        {
            var database = dbContext.Database;
            var dbConnection = (SqlConnection)database.GetDbConnection();
            var command = new SqlCommand(
                $"SELECT {size} FROM ImageData WHERE Id = @id;",
                dbConnection);

            command.Parameters.Add(new SqlParameter("@id", id));

            dbConnection.Open();

            var reader = await command.ExecuteReaderAsync();

            Stream result = null;

            if (reader.HasRows)
            {
                while (reader.Read()) result = reader.GetStream(0);
            }

            reader.Close();

            return result;
        }
    }
}
