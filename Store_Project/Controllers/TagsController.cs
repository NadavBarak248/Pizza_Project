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
    public class TagsController : Controller
    {
        private readonly Store_ProjectContext _context;

        public TagsController(Store_ProjectContext context)
        {
            _context = context;
        }


        private void SetPizzaListItemsAsync(int[] pizzasId=null)
        {
           ViewBag.pizzas = new MultiSelectList(_context.Pizza, nameof(Pizza.Id), nameof(Pizza.Name), pizzasId);
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
            IOrderedQueryable<Tag> q = from t in _context.Tag.Include(t => t.Pizza_tag)
                                       orderby t.Pizza_tag.Count descending
                                       select t;
            return View(await q.ToListAsync());
        }

        // GET: Tags/Search
        public async Task<IActionResult> Search(string searchquery)
        {
            IOrderedQueryable<Tag> q = from t in _context.Tag.Include(t => t.Pizza_tag)
                                       where
                                       t.Name.Contains(searchquery) || (searchquery == null)
                                       orderby t.Pizza_tag.Count descending
                                       select t;

            return Json(await q.ToListAsync());
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            SetPizzaListItemsAsync();
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Tag tag, int[] Pizza_tag)
        {
            if (ModelState.IsValid)
            {
                tag.Pizza_tag = new List<Pizza>();
                tag.Pizza_tag.AddRange(_context.Pizza.Where(x => Pizza_tag.Contains(x.Id)));

                _context.Add(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag.Include(t => t.Pizza_tag).FirstOrDefaultAsync(e => e.Id == id);
            int[] pizzasId= tag.Pizza_tag.Select(p => p.Id).ToArray();
            SetPizzaListItemsAsync(pizzasId);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Tag tag, int[] Pizza_tag)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Remove existing pizzas
                    Tag tt = await _context.Tag.Include(t => t.Pizza_tag).SingleOrDefaultAsync(t => t.Id == id);
                    if (tt != null)
                    {
                        foreach (Pizza p in tt.Pizza_tag.ToList())
                        {
                            tt.Pizza_tag.Remove(p);
                        }
                        await _context.SaveChangesAsync();
                    }
                    _context.Entry(tt).State = EntityState.Detached;

                    // adding new tags selected
                    tag.Pizza_tag = new List<Pizza>();
                    tag.Pizza_tag.AddRange(_context.Pizza.Where(p => Pizza_tag.Contains(p.Id)));




                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.Id))
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
            return View(tag);
        }


        // Get: Tags/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _context.Tag.FindAsync(id);
            _context.Tag.Remove(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(int id)
        {
            return _context.Tag.Any(e => e.Id == id);
        }
    }
}
