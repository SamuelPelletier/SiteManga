using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteManga.Data;
using SiteManga.Models;
using SiteManga.Repository;

namespace SiteManga
{
    [Authorize(Roles = "admin")]
    public class MangaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MangaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mangas
        public async Task<IActionResult> Index()
        {

            return View(await _context.Mangas.Include(x => x.Editor).ToListAsync());
        }

        // GET: Mangas/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manga = await _context.Mangas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }

        // GET: Mangas/Create
        public IActionResult Create()
        {
            var editors = _context.Editors;
            ViewBag.Editors = editors;
            ImageRepository imageRepository = new ImageRepository(this._context);
            var images = imageRepository.getImagesNotBind();
            ViewBag.Images = images;
            return View();
        }

        // POST: Mangas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ShortDescription,LongDescription,Height,Width,Length,Color,Weight,Price,NumberOfPages")] Manga manga)
        {
            if (ModelState.IsValid)
            {
                Editor editor = _context.Editors.Find(int.Parse(HttpContext.Request.Form["Editor"]));
                manga.Editor = editor;
                var imagesId = HttpContext.Request.Form["Images"].ToArray();
                List<Image> images = new List<Image>();
                foreach (string imageId in imagesId)
                {
                    images.Add(_context.Images.Find(int.Parse(imageId)));
                }
                manga.Images = images;

                _context.Add(manga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manga);
        }

        // GET: Mangas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manga = await _context.Mangas.FindAsync(id);

            var editors = _context.Editors;
            ViewBag.Editors = editors;
            ImageRepository imageRepository = new ImageRepository(this._context);
            List<Image> images = imageRepository.getImagesNotBind();
            images = images.Concat(manga.Images).ToList();
            ViewBag.Images = images;

            if (manga == null)
            {
                return NotFound();
            }
            return View(manga);
        }

        // POST: Mangas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShortDescription,LongDescription,Height,Width,Length,Color,Weight,Price,NumberOfPages")] Manga manga)
        {
            if (id != manga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Editor editor = _context.Editors.Find(int.Parse(HttpContext.Request.Form["Editor"]));
                manga.Editor = editor;
                var imagesId = HttpContext.Request.Form["Images"].ToArray();
                List<Image> images =  new List<Image>();
                foreach(string imageId in imagesId)
                {
                    images.Add(_context.Images.Find(int.Parse(imageId)));
                }
                manga.Images = images;

                try
                {
                    _context.Update(manga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MangaExists(manga.Id))
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
            return View(manga);
        }

        // GET: Mangas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manga = await _context.Mangas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }

        // POST: Mangas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manga = await _context.Mangas.FindAsync(id);
            _context.Mangas.Remove(manga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MangaExists(int id)
        {
            return _context.Mangas.Any(e => e.Id == id);
        }
    }
}
