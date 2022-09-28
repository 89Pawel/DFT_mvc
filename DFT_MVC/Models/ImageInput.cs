using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace DFT_MVC.Models
{
    public class ImageInput
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Stream Content { get; set; }
    }
}
