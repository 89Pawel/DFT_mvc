using DFT_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DFT_MVC.Models
{
    public class Subcategory
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.Date)]
        [HiddenInput]
        public DateTime CreationDate { get; set; } = DateTime.Today;
        public byte[]? ImageOriginal { get; set; } = null;
        [Display(Name = "Zdjęcie")]
        public byte[]? ImageBig { get; set; } = null;
        [Display(Name = "Zdjęcie")]
        public byte[]? ImageSmall { get; set; } = null;
        public Category? Category { get; set; }
        [Required]
        public int CategoryId { get; set; }
        //public ImageData? ImageData { get; set; }
    }
}
