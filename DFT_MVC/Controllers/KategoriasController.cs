namespace DFT_MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using DFT_MVC.Data;
    using DFT_MVC.Models;
    using DFT_MVC.Services;
    using System.Diagnostics;
    using DFT_MVC.ViewModel;

    public class KategoriasController : Controller
    {
        private readonly IImageService _imageService;
        private readonly DFT_MVC_Context _context;
        private readonly IKategorieService _kategorieService;

        public KategoriasController(DFT_MVC_Context context, IImageService imageService, IKategorieService kategorieService)
        {
            _context = context;
            _imageService = imageService;
            _kategorieService = kategorieService;
        }

        // GET: Kategories
        public async Task<IActionResult> Index()
        {
            var kategorie = await _context.Kategoria.ToArrayAsync();
            var images = await _context.ImageData.ToArrayAsync();
            //var imageService = await _context.ImageData.Select(i => i.Id.ToString()).ToListAsync();
            //var test = _context.Kategoria.Include(i => i.ImageData).AsNoTracking();
            //var image = _context.ImageData.Single(i => i.KategoriaId == );
            _kategorieService.Kategorie = kategorie;
            _kategorieService.Images = images;
            //_kategorieViewModel.Kategorie = kategorie;
            //_kategorieViewModel.Images = images;

            //_kategorieService.ImageServices = imageService;

            return View(_kategorieService);
        }

        // GET: Kategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kategoria == null)
            {
                return NotFound();
            }

            var kategorie = await _context.Kategoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategorie == null)
            {
                return NotFound();
            }
            //var test = await _context.ImageData.FirstOrDefaultAsync(m => m.KategoriaId == id);
            var test = _context.ImageData.Single(i => i.KategoriaId == kategorie.Id);

            Debug.WriteLine("QQQQQQQQQQQQQQQQQQQ " + test.OriginalFileName);

            return View(kategorie);
        }

        // GET: Kategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreationDate")] Kategoria kategorie, IFormFile[] images)
        {
            if (ModelState.IsValid)
            {
                kategorie.CreationDate = DateTime.Today;
                //kategorie.ImageData = new ImageData
                //{
                //    OriginalFileName = "dupa",
                //    OriginalType = "jpg",
                //    OriginalContent = new byte[1],
                //    FullscreenContent = new byte[1],
                //    ThumbnailBigContent = new byte[1],
                //    ThumbnailSmallContent = new byte[1],
                //    KategoriaId = 999
                //};
                _context.Add(kategorie);

                await _context.SaveChangesAsync();

                if (images.Length > 1)
                {
                    ModelState.AddModelError("images", "Nie możesz dodać więcej jak 10 zdjęć!");
                }
                await _imageService.Process(images.Select(i => new ImageInput
                {
                    Name = i.FileName,
                    Type = i.ContentType,
                    Content = i.OpenReadStream()
                }),
                    kategorie.Id
                );

                return RedirectToAction(nameof(Index));
            }
            return View(kategorie);
        }

        // GET: Kategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kategoria == null)
            {
                return NotFound();
            }

            var kategorie = await _context.Kategoria.FindAsync(id);
            if (kategorie == null)
            {
                return NotFound();
            }
            return View(kategorie);
        }

        // POST: Kategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreationDate")] Kategoria kategorie)
        {
            if (id != kategorie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategorie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategorieExists(kategorie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kategorie);
        }

        // GET: Kategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kategoria == null)
            {
                return NotFound();
            }

            var kategorie = await _context.Kategoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategorie == null)
            {
                return NotFound();
            }

            return View(kategorie);
        }

        // POST: Kategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kategoria == null)
            {
                return Problem("Entity set 'DFT_MVC_Context.Kategorie'  is null.");
            }
            var kategorie = await _context.Kategoria.FindAsync(id);
            if (kategorie != null)
            {
                _context.Kategoria.Remove(kategorie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategorieExists(int id)
        {
          return _context.Kategoria.Any(e => e.Id == id);
        }
    }
}
