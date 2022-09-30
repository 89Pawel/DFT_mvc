namespace DFT_MVC.Models
{
    using DFT_MVC.Data;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    public class Kategoria
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [HiddenInput]
        public DateTime CreationDate {get; set; }
        public ImageData ImageData { get; set; }
    }
}
