using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DFT_MVC.Models
{
    public class Kategorie
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        [HiddenInput]
        public DateTime CreationDate {get; set; }
    }
}
