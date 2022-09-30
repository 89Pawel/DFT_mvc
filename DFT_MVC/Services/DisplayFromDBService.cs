namespace DFT_MVC.Services
{
    using DFT_MVC.Data;
    using DFT_MVC.Models;
    using Microsoft.EntityFrameworkCore;

    public class DisplayFromDBService : IDisplayFromDBService
    {
        private readonly DFT_MVC_Context _context;

        public DisplayFromDBService(DFT_MVC_Context context)
        {
            _context = context;
        }
        public IEnumerable<Kategoria> Kategorie;
        public IEnumerable<ImageData> Images;

        public async Task GetData()
        {
            Kategorie = await _context.Kategoria.ToListAsync();
            Images = await _context.ImageData.ToListAsync();
        }

        public async Task<IEnumerable<ImageData>> GetImages() => Images = await _context.ImageData.ToListAsync();

        public async Task<IEnumerable<Kategoria>> GetKategorie() => Kategorie = await _context.Kategoria.ToListAsync();

        public async Task<ImageData> GetOneImage(int id)
        {
            var image = await _context.ImageData.FirstOrDefaultAsync(i => i.KategoriaId == id);

            return image;
        }
    }
}
