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
    public class PizzaImagesController : Controller
    {
        private readonly Store_ProjectContext _context;

        public PizzaImagesController(Store_ProjectContext context)
        {
            _context = context;
        }

        // GET: PizzaImages
        public async Task<IActionResult> Index()
        {
            var store_ProjectContext = _context.PizzaImage.Include(p => p.Pizza);
            return View(await store_ProjectContext.ToListAsync());
        }

        // GET: PizzaImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaImage = await _context.PizzaImage
                .Include(p => p.Pizza)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaImage == null)
            {
                return NotFound();
            }

            return View(pizzaImage);
        }

        // GET: PizzaImages/Create
        public IActionResult Create()
        {
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "Id");
            return View();
        }

        // POST: PizzaImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,PizzaId")] PizzaImage pizzaImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pizzaImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "Id", pizzaImage.PizzaId);
            return View(pizzaImage);
        }

        // GET: PizzaImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaImage = await _context.PizzaImage.FindAsync(id);
            if (pizzaImage == null)
            {
                return NotFound();
            }
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "Id", pizzaImage.PizzaId);
            return View(pizzaImage);
        }

        // POST: PizzaImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,PizzaId")] PizzaImage pizzaImage)
        {
            if (id != pizzaImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizzaImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaImageExists(pizzaImage.Id))
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
            ViewData["PizzaId"] = new SelectList(_context.Pizza, "Id", "Id", pizzaImage.PizzaId);
            return View(pizzaImage);
        }

        // GET: PizzaImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzaImage = await _context.PizzaImage
                .Include(p => p.Pizza)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaImage == null)
            {
                return NotFound();
            }

            return View(pizzaImage);
        }

        // POST: PizzaImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pizzaImage = await _context.PizzaImage.FindAsync(id);
            _context.PizzaImage.Remove(pizzaImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaImageExists(int id)
        {
            return _context.PizzaImage.Any(e => e.Id == id);
        }
    }
}
