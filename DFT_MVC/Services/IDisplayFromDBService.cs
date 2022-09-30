namespace DFT_MVC.Services
{
    using DFT_MVC.Data;
    using DFT_MVC.Models;

    public interface IDisplayFromDBService
	{
        public Task<IEnumerable<Kategoria>> GetKategorie();
        public Task<IEnumerable<ImageData>> GetImages();
        public Task GetData();
    }
}
