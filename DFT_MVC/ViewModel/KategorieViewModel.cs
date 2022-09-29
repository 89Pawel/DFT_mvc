using DFT_MVC.Data;
using DFT_MVC.Models;
using DFT_MVC.Services;
using NuGet.DependencyResolver;

namespace DFT_MVC.ViewModel
{
    public class KategorieViewModel : IKategorieService
    {
        public IEnumerable<Kategoria> Kategorie { get; set; }
        public IEnumerable<ImageData> Images { get; set; }
        //public List<string> ImageServices { get; set; }

    }
}
