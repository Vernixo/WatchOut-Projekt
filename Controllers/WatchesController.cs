using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WatchOut.Data;
using WatchOut.Models;

namespace WatchOut.Controllers
{
    public class WatchesController : Controller
    {
        private readonly WatchOutContext _context;

        public WatchesController(WatchOutContext context)
        {
            _context = context;
        }

        // GET: Watches
        public async Task<IActionResult> Index()
        {
            return View(await _context.Watch.ToListAsync());
        }

        // GET: Watches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watch = await _context.Watch
                .FirstOrDefaultAsync(m => m.Id == id);
            if (watch == null)
            {
                return NotFound();
            }

            return View(watch);
        }

        // GET: Watches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Watches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,Name,Price,Size,UserGender,Style,Quantity,PhotoPath")] Watch watch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(watch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(watch);
        }

        // GET: Watches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watch = await _context.Watch.FindAsync(id);
            if (watch == null)
            {
                return NotFound();
            }
            return View(watch);
        }

        // POST: Watches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Name,Price,Size,UserGender,Style,Quantity,PhotoPath")] Watch watch)
        {
            if (id != watch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(watch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchExists(watch.Id))
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
            return View(watch);
        }

        // GET: Watches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watch = await _context.Watch
                .FirstOrDefaultAsync(m => m.Id == id);
            if (watch == null)
            {
                return NotFound();
            }

            return View(watch);
        }

        // POST: Watches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var watch = await _context.Watch.FindAsync(id);
            if (watch != null)
            {
                _context.Watch.Remove(watch);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WatchExists(int id)
        {
            return _context.Watch.Any(e => e.Id == id);
        }
    }
}
