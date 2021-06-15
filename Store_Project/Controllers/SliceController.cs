using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store_Project.Data;
using Store_Project.Models;

namespace Store_Project.Controllers
{
    public class SliceController : Controller
    {
        private readonly Store_ProjectContext _context;

        public SliceController(Store_ProjectContext context)
        {
            _context = context;
        }

        // GET: Slice
        public async Task<IActionResult> Index()
        {
            var store_ProjectContext = _context.Slice.Include(s => s.Pizza);
            return View(await store_ProjectContext.ToListAsync());
        }

        // GET: Slice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slice = await _context.Slice
                .Include(s => s.Pizza)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slice == null)
            {
                return NotFound();
            }

            return View(slice);
        }

        // GET: Slice/Create
        public IActionResult Create()
        {
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "Id");
            return View();
        }

        // POST: Slice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PizzaId,Orders_number")] Slice slice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(slice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "Id", slice.PizzaId);
            return View(slice);
        }

        // GET: Slice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slice = await _context.Slice.FindAsync(id);
            if (slice == null)
            {
                return NotFound();
            }
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "Id", slice.PizzaId);
            return View(slice);
        }

        // POST: Slice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PizzaId,Orders_number")] Slice slice)
        {
            if (id != slice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliceExists(slice.Id))
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
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "Id", slice.PizzaId);
            return View(slice);
        }

        // GET: Slice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slice = await _context.Slice
                .Include(s => s.Pizza)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slice == null)
            {
                return NotFound();
            }

            return View(slice);
        }

        // POST: Slice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slice = await _context.Slice.FindAsync(id);
            _context.Slice.Remove(slice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliceExists(int id)
        {
            return _context.Slice.Any(e => e.Id == id);
        }
    }
}
