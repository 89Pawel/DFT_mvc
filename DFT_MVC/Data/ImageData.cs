namespace DFT_MVC.Data
{
    using DFT_MVC.Models;
    using System.ComponentModel.DataAnnotations;

    public class ImageData
    {
        public ImageData() => Id = Guid.NewGuid();

        public Guid Id { get; set; }
        public string OriginalFileName { get; set; }
        public string OriginalType { get; set; }
        public byte[] OriginalContent { get; set; }
        [Display(Name = "Zdjęcie")]
        public byte[] FullscreenContent { get; set; }
        [Display(Name = "Zdjęcie")]
        public byte[] ThumbnailBigContent { get; set; }
        [Display(Name = "Zdjęcie")]
        public byte[] ThumbnailSmallContent { get; set; }

        public int KategoriaId { get; set; }
        public Kategoria Kategoria { get; set; }
    }
}
