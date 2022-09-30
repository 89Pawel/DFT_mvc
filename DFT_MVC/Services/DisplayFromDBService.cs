namespace DFT_MVC.Services
{
    using DFT_MVC.Data;
    using DFT_MVC.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class DisplayFromDBService : IDisplayFromDBService
    {
        private readonly DFT_MVC_Context _context;

        public DisplayFromDBService(DFT_MVC_Context context)
        {
            _context = context;
        }
        public IEnumerable<Kategoria> Kategorie;
        public IEnumerable<ImageData> Images;
        public Dictionary<Kategoria, ImageData> Display;


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

        public async Task<Dictionary<Kategoria, ImageData>> GetDataDict()
        {
            var dict = new Dictionary<Kategoria, ImageData>();
            var kat = await GetKategorie();
            var zdj = await GetImages();


            for (int i = 0; i < kat.ToList().Count; i++)
            {
                var key = kat.ToList()[i];
                var value = zdj.FirstOrDefault(x => x.KategoriaId == kat.ToList()[i].Id);

                if (value != null)
                {
                dict.Add(key, value);

                }
                else
                {
                    value = new ImageData() { };
                    dict.Add(key, value);

                }
                //Debug.WriteLine(dict[i]);
            }

            return Display = dict;
        }
    }
}
