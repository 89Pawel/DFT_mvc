namespace DFT_MVC.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using DFT_MVC.Data;
    using DFT_MVC.Models;
    using DFT_MVC.Services;
    using System.Diagnostics;

    public class KategoriasController : Controller
    {
        private readonly IImageService _imageService;
        private readonly DFT_MVC_Context _context;
        //private readonly IDisplayFromDBService _displayFromDBService;
        private readonly IAlertService _alertService;
        private readonly ImagesController _imagesController;

        public KategoriasController(DFT_MVC_Context context, IImageService imageService, IAlertService alertService, ImagesController imagesController)
        {
            _context = context;
            _imageService = imageService;
            //_displayFromDBService = displayFromDBService;
            _alertService = alertService;
            _imagesController = imagesController;
        }

        // GET: Kategories
        //public async Task<IActionResult> Index()
        //{
        //    await _displayFromDBService.GetDataDict();
        //    return View(_displayFromDBService);
        //}
        public async Task<IActionResult> Index()
        {
            var kategorie = await _context.Kategoria.Include(i => i.ImageData).ToListAsync();
            //await _displayFromDBService.GetDataDict();
            return View(kategorie);
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

            //var test = await _displayFromDBService.GetDataDict();

            //foreach (var item in test)
            //{
            //    Debug.WriteLine(item.Key.Name+": "+item.Key.Id+" || "+ item.Value.KategoriaId+": "+item.Value.OriginalFileName);

            //}
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
                
                _context.Add(kategorie);
                await _context.SaveChangesAsync();
                await _imagesController.Upload(images, kategorie.Id);

                //await _imageService.Process(images.Select(i => new ImageInput
                //{
                //    Name = i.FileName,
                //    Type = i.ContentType,
                //    Content = i.OpenReadStream()
                //}),
                //    kategorie.Id
                //);

                TempData["ResultMessage"] = _alertService.TempDataAlert(kategorie.Name, 1);

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

                TempData["ResultMessage"] = _alertService.TempDataAlert(kategorie.Name, 2);

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

            TempData["ResultMessage"] = _alertService.TempDataAlert(kategorie.Name, 3);

            return RedirectToAction(nameof(Index));
        }

        private bool KategorieExists(int id)
        {
          return _context.Kategoria.Any(e => e.Id == id);
        }
    }
}
