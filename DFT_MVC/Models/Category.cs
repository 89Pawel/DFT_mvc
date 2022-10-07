namespace DFT_MVC.Models
{
    using DFT_MVC.Data;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        [HiddenInput]
        public DateTime CreationDate {get; set; } = DateTime.Today;
        public ImageData? ImageData { get; set; }
        public List<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
    }
}
