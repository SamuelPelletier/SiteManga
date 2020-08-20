﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteManga.Data;
using SiteManga.Models;

namespace SiteManga
{
    [Authorize(Roles = "admin")]
    public class EditorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EditorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Editors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Editors.ToListAsync());
        }

        // GET: Editors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editor = await _context.Editors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editor == null)
            {
                return NotFound();
            }

            return View(editor);
        }

        // GET: Editors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Editors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Editor editor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(editor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(editor);
        }

        // GET: Editors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editor = await _context.Editors.FindAsync(id);
            if (editor == null)
            {
                return NotFound();
            }
            return View(editor);
        }

        // POST: Editors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Editor editor)
        {
            if (id != editor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorExists(editor.Id))
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
            return View(editor);
        }

        // GET: Editors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editor = await _context.Editors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editor == null)
            {
                return NotFound();
            }

            return View(editor);
        }

        // POST: Editors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var editor = await _context.Editors.FindAsync(id);
            _context.Editors.Remove(editor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditorExists(int id)
        {
            return _context.Editors.Any(e => e.Id == id);
        }
    }
}
