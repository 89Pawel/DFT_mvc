namespace DFT_MVC.Data
{
    using DFT_MVC.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public class ImageData
    {
        public ImageData() => Id = Guid.NewGuid();
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? OriginalFileName { get; set; }
        [Required]
        public string? OriginalType { get; set; }
        [Required]
        public byte[]? OriginalContent { get; set; }
        [Required]
        [Display(Name = "Zdjęcie")]
        public byte[]? FullscreenContent { get; set; }
        [Required]
        [Display(Name = "Zdjęcie")]
        public byte[]? ThumbnailBigContent { get; set; }
        [Required]
        [Display(Name = "Zdjęcie")]
        public byte[]? ThumbnailSmallContent { get; set; }

        public Subcategory? Subcategory { get; set; }
        public int? SubcategoryId { get; set; }
    }
}
