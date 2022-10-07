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

    public class CategoriesController : Controller
    {
        private readonly IImageService _imageService;
        private readonly DFT_MVC_Context _context;
        //private readonly IDisplayFromDBService _displayFromDBService;
        private readonly IAlertService _alertService;
        private readonly ImagesController _imagesController;

        public CategoriesController(DFT_MVC_Context context, IImageService imageService, IAlertService alertService, ImagesController imagesController)
        {
            _context = context;
            _imageService = imageService;
            //_displayFromDBService = displayFromDBService;
            _alertService = alertService;
            _imagesController = imagesController;
        }

        // GET: Categories
        //public async Task<IActionResult> Index()
        //{
        //    await _displayFromDBService.GetDataDict();
        //    return View(_displayFromDBService);
        //}
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Include(i => i.ImageData).Include(i => i.Subcategories).ToListAsync();
            //await _displayFromDBService.GetDataDict();
            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            //var test = await _displayFromDBService.GetDataDict();

            //foreach (var item in test)
            //{
            //    Debug.WriteLine(item.Key.Name+": "+item.Key.Id+" || "+ item.Value.KategoriaId+": "+item.Value.OriginalFileName);

            //}
            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreationDate")] Category category, IFormFile[] images)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                await _imagesController.Upload(images, categoryId: category.Id);

                //await _imageService.Process(images.Select(i => new ImageInput
                //{
                //    Name = i.FileName,
                //    Type = i.ContentType,
                //    Content = i.OpenReadStream()
                //}),
                //    kategorie.Id
                //);

                TempData["ResultMessage"] = _alertService.TempDataAlert(category.Name!, 1);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreationDate")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["ResultMessage"] = _alertService.TempDataAlert(category.Name!, 2);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'DFT_MVC_Context.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);

            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();

            TempData["ResultMessage"] = _alertService.TempDataAlert(category!.Name!, 3);

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return _context.Categories.Any(e => e.Id == id);
        }
    }
}
