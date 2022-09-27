namespace DFT_MVC.Services
{
    using DFT_MVC.Models;
    using Microsoft.AspNetCore.Http;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.ImageSharp.Processing;

    public class ImageService : IImageService
    {
        private const int ThumbnailBigWidth = 480;
        private const int ThumbnailSmallWidth = 150;
        private const int FullscreenWidth = 1024;

        public async Task Process(IEnumerable<ImageInput> images)
        {
            var tasks = images.Select(image => Task.Run(async () =>
            {
                using var imgResult = await Image.LoadAsync(image.Content);

                await SaveImage(imgResult, $"Original_{image.Name}", imgResult.Width);
                await SaveImage(imgResult, $"Fullscreen{image.Name}", FullscreenWidth);
                await SaveImage(imgResult, $"ThumbnailBig{image.Name}", ThumbnailBigWidth);
                await SaveImage(imgResult, $"ThumbnailSmall{image.Name}", ThumbnailSmallWidth);

            })).ToList();

            await Task.WhenAll(tasks);
        }

        private async Task SaveImage(Image image, string name, int resizeWidth)
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

            await image.SaveAsJpegAsync(name, new JpegEncoder
            {
                Quality = 75
            });
        }
    }
}
