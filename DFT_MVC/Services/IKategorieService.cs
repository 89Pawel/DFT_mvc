using DFT_MVC.Data;
using DFT_MVC.Models;

namespace DFT_MVC.Services
{
	public interface IKategorieService
	{
        public IEnumerable<Kategoria> Kategorie { get; set; }
        public IEnumerable<ImageData> Images { get; set; }
        //public List<string> ImageServices { get; set; }



    }
}
