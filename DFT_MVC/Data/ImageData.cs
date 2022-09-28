using DFT_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DFT_MVC.Data
{
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

        public int KategorieId { get; set; }
        public Kategorie Kategorie { get; set; }
    }
}
