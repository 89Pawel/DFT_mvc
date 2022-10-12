using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DFT_MVC.Data;
using DFT_MVC.Models;
using System.Diagnostics;
using DFT_MVC.Services;
using System.Xml.Linq;

namespace DFT_MVC.Controllers
{
    public class SubcategoriesController : Controller
    {
        private readonly DFT_MVC_Context _context;
        private readonly ImagesController _imagesController;
        private readonly ISubcategoryService _subcategoryService;
        public int TEST { get; set; }

        public SubcategoriesController(DFT_MVC_Context context, ImagesController imagesController, ISubcategoryService subcategoryService)
        {
            _context = context;
            _imagesController = imagesController;
            _subcategoryService = subcategoryService;
        }

        public async Task<IActionResult> ImageSmall(string id) => File(await _subcategoryService.GetImageSmall(id), "image/jpeg");
        public async Task<IActionResult> ImageBig(string id) => File(await _subcategoryService.GetImageBig(id), "image/jpeg");

        // GET: Subcategories
        [Route("Subcategory/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var dFT_MVC_Context = _context.Subcategories!.Include(s => s.Category).Where(i => i.CategoryId == id);

            var selectedCategory = await _context.Categories.SingleAsync(i => i.Id == id);
            ViewData["CategoryId"] = selectedCategory.Id;
            return View(await dFT_MVC_Context.ToListAsync());
        }

        // GET: Subcategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Subcategories == null)
            {
                return NotFound();
            }

            var subcategory = await _context.Subcategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subcategory == null)
            {
                return NotFound();
            }

            return View(subcategory);
        }

        // GET: Subcategories/Create
        public IActionResult Create(int? id)
        {
            var categoryInfo = _context.Categories.Find(id);
            ViewData["CategoryId"] = categoryInfo!.Id;
            ViewData["CategoryName"] = categoryInfo!.Name;
            return View();
        }

        // POST: Subcategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CreationDate,CategoryId")] Subcategory subcategory, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                await _subcategoryService.CreateSubcategory(subcategory, image);

                return RedirectToAction(nameof(Index), new { id = subcategory.CategoryId });
            }
            return View(subcategory);
        }

        // GET: Subcategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subcategories == null)
            {
                return NotFound();
            }

            var subcategory = await _context.Subcategories.FindAsync(id);
            if (subcategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = subcategory.CategoryId;
            return View(subcategory);
        }

        // POST: Subcategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Description, CreationDate, ImageOriginal, ImageBig, ImageSmall, CategoryId")] Subcategory subcategory, IFormFile? image)
        {
            if (id != subcategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _subcategoryService.UpdateSubcategory(subcategory!, image);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubcategoryExists(subcategory!.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = subcategory!.CategoryId });
            }
            return View(subcategory);
        }

        // GET: Subcategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subcategories == null)
            {
                return NotFound();
            }

            var subcategory = await _context.Subcategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subcategory == null)
            {
                return NotFound();
            }

            return View(subcategory);
        }

        // POST: Subcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Subcategories == null)
            {
                return Problem("Entity set 'DFT_MVC_Context.Subcategories'  is null.");
            }
            var subcategory = await _context.Subcategories.SingleAsync(i => i.Id == id);
            if (subcategory != null)
            {
                _context.Subcategories.Remove(subcategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = subcategory!.CategoryId });
        }

        private bool SubcategoryExists(int id)
        {
          return _context.Subcategories!.Any(e => e.Id == id);
        }
    }
}
