using DFT_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DFT_MVC.Models
{
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
