using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store_Project.Data;
using Store_Project.Models;

namespace Store_Project.Controllers
{
    //[Authorize(Roles = "Manager")]
    public class ToppingsController : Controller
    {
        private readonly Store_ProjectContext _context;

        public ToppingsController(Store_ProjectContext context)
        {
            _context = context;
        }

        private void SetPizzaListItemsAsync(int[] pizzasId = null)
        {
            ViewBag.pizzas = new MultiSelectList(_context.Pizza, nameof(Pizza.Id), nameof(Pizza.Name), pizzasId);
        }

        // GET: Toppings
        public async Task<IActionResult> Index()
        {
            IOrderedQueryable<Topping> q = from t in _context.Topping.Include(t => t.Toppings_pizza)
                                       orderby t.Toppings_pizza.Count descending
                                       select t;
            return View(await q.ToListAsync());
        }

        public async Task<IActionResult> Search(string searchquery)
        {
            IOrderedQueryable<Topping> q = from t in _context.Topping.Include(t => t.Toppings_pizza)
                                       where
                                       t.Name.Contains(searchquery) || (searchquery == null)
                                       orderby t.Toppings_pizza.Count descending
                                       select t;

            return Json(await q.ToListAsync());
        }


        // GET: Toppings/Create
        public IActionResult Create()
        {
            SetPizzaListItemsAsync();
            return View();
        }

        // POST: Toppings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price")] Topping topping, int[] Toppings_pizza)
        {
            if (ModelState.IsValid)
            {
                topping.Toppings_pizza = new List<Pizza>();
                topping.Toppings_pizza.AddRange(_context.Pizza.Where(x => Toppings_pizza.Contains(x.Id)));

                foreach (int pid in Toppings_pizza)
                {
                    // adding new topping price
                    Pizza p = _context.Pizza.Single(p => p.Id == pid);
                    p.Price += topping.Price;
                    _context.Update(p);
                }

                _context.Add(topping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topping);
        }

        // GET: Toppings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = await _context.Topping.Include(t => t.Toppings_pizza).FirstOrDefaultAsync(e => e.Id == id);
            int[] pizzasId = topping.Toppings_pizza.Select(p => p.Id).ToArray();
            SetPizzaListItemsAsync(pizzasId);
            if (topping == null)
            {
                return NotFound();
            }
            return View(topping);
        }


        // POST: Toppings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price")] Topping topping, int[] Toppings_pizza)
        {
            if (id != topping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Remove existing pizzas
                    Topping tp = await _context.Topping.Include(t => t.Toppings_pizza).SingleOrDefaultAsync(t => t.Id == id);
                    if (tp != null)
                    {
                        foreach (Pizza p in tp.Toppings_pizza.ToList())
                        {
                            // decreasing old toppings price
                            p.Price -= tp.Price;
                            _context.Update(p);
                            tp.Toppings_pizza.Remove(p);
                        }
                        await _context.SaveChangesAsync();
                    }
                    _context.Entry(tp).State = EntityState.Detached;

                    // adding new tags selected
                    topping.Toppings_pizza = new List<Pizza>();
                    topping.Toppings_pizza.AddRange(_context.Pizza.Where(p => Toppings_pizza.Contains(p.Id)));

                    foreach(int pid in Toppings_pizza)
                    {
                        // adding new topping price
                        Pizza p = _context.Pizza.Single(p => p.Id == pid);
                        p.Price += topping.Price;
                        _context.Update(p);
                    }

                    _context.Update(topping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToppingExists(topping.Id))
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
            return View(topping);
        }

        // GET: Toppings/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var topping = _context.Topping.Include(t => t.Toppings_pizza).Single(t => t.Id == id);
            foreach (Pizza p in topping.Toppings_pizza.ToList())
            {
                // decreasing old toppings price
                p.Price -= topping.Price;
                _context.Update(p);
            }
            _context.Topping.Remove(topping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ToppingExists(int id)
        {
            return _context.Topping.Any(e => e.Id == id);
        }
    }
}
