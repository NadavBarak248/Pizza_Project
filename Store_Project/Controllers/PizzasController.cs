using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store_Project.Data;
using Store_Project.Models;

namespace Store_Project.Controllers
{
    public class PizzasController : Controller
    {
        private readonly Store_ProjectContext _context;
        private double curVal = 1;
        private string curName = "ILS";

        public PizzasController(Store_ProjectContext context)
        {
            _context = context;
        }

        private List<SelectList> GetSelectListsFromEnums()
        {
            var sizeEnum = from Size s in Enum.GetValues(typeof(Size))
            select new
            {
                ID = (int)s,
                Name = s.ToString()
            };

            var widthEnum = from Width w in Enum.GetValues(typeof(Width))
            select new
            {
                ID = (int)w,
                Name = w.ToString()
            };

            var sauceEnum = from Sauce sa in Enum.GetValues(typeof(Sauce))
            select new
            {
                ID = (int)sa,
                Name = sa.ToString()
            };

            List<SelectList> EnumsList = new List<SelectList>();
            EnumsList.Add(new SelectList(sizeEnum, "ID", "Name"));
            EnumsList.Add(new SelectList(widthEnum, "ID", "Name"));
            EnumsList.Add(new SelectList(sauceEnum, "ID", "Name"));

            return EnumsList;

        }

        private void BuildEmptyFieldsViewData(int[] selectedTags=null)
        {
            // Enums
            List<SelectList> EnumsList = GetSelectListsFromEnums();
            ViewBag.pizzaSizes = EnumsList[0];
            ViewBag.pizzaWidths = EnumsList[1];
            ViewBag.pizzaSauces = EnumsList[2];

            // Tags
            ViewBag.tags = new MultiSelectList(_context.Tag, nameof(Tag.Id), nameof(Tag.Name), selectedTags);
        }

        // GET: Pizzas
        public async Task<IActionResult> Index()
        {
            BuildEmptyFieldsViewData();
            IOrderedQueryable<Pizza> q = from p in _context.Pizza.Include(p => p.Pizza_tags).Include(p => p.Pizza_image)
                                         where
                                         (p.To_present == true)

                                         orderby p.Price descending
                                         select p;

            return View(await q.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string searchquery, int[] tagids, int[] sauceid, double pricelimit, string currencyTo)
        {
            if (currencyTo != curName)
            {
                curName = currencyTo;
                if (curName != "ILS")
                    curVal = await CurrencyConverter("ILS", curName, 1);
                else
                    curVal = 1;
            }
            IOrderedQueryable<Pizza> q = from p in _context.Pizza.Include(p => p.Pizza_tags).Include(p => p.Pizza_image)
                                         where 
                                         (p.To_present == true) &&
                                         (p.Name.Contains(searchquery) || (searchquery == null)) &&
                                         ((sauceid.Length == 0) || (sauceid.Contains((int)p.Pizza_sauce))) &&
                                         ((tagids.Length == 0) || (p.Pizza_tags.Any(x => tagids.Contains(x.Id)))) &&
                                         ((pricelimit >= p.Price * curVal) || (pricelimit <= 0))

                                         orderby p.Price descending
                                         select p;

            List<Pizza> lp = await q.ToListAsync();
            lp.ForEach(p => p.Price = Math.Round(p.Price * curVal, 2));
            
            return Json(lp);
        }

        public async Task<double> CurrencyConverter(string from, string to, double amount)
        {
            Currency.CurrencyServerSoapClient.EndpointConfiguration ec = new Currency.CurrencyServerSoapClient.EndpointConfiguration();
            Currency.CurrencyServerSoapClient con = new Currency.CurrencyServerSoapClient(ec);
            return await con.ConvertToNumAsync("", from, to, amount, false, "", "");
        }

        // GET: Pizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizza
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // GET: Pizzas/Create
        public IActionResult Create()
        {
            BuildEmptyFieldsViewData();
            return View();
        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Pizza_size,Slices_number,Pizza_width,Pizza_sauce,With_cheese, To_present")] Pizza pizza, int[] Pizza_tags, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                
                pizza.Pizza_tags = new List<Tag>();
                pizza.Pizza_tags.AddRange(_context.Tag.Where(x => Pizza_tags.Contains(x.Id)));

                if (ImageFile != null)
                {
                    pizza.Pizza_image = new PizzaImage();
                    pizza.Pizza_image.ImageFile = ImageFile;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pizza.Pizza_image.ImageFile.CopyTo(ms);
                        pizza.Pizza_image.Image_content = ms.ToArray();
                    }
                }
                


                _context.Add(pizza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pizza);
        }

        // GET: Pizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizza.Include(p => p.Pizza_tags).Include(p => p.Pizza_image).FirstOrDefaultAsync(e => e.Id == id);
            int[] tagids = pizza.Pizza_tags.Select(tag => tag.Id).ToArray();
            BuildEmptyFieldsViewData(tagids);
            if (pizza.Pizza_image != null)
                ViewBag.Pizza_image = pizza.Pizza_image.Image_content;
            
            if (pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Pizza_size,Slices_number,Pizza_width,Pizza_sauce,With_cheese, To_present")] Pizza pizza, int[] Pizza_tags, IFormFile ImageFile)
        {
            if (id != pizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Remove existing tags
                    Pizza pp = await _context.Pizza.Include(p => p.Pizza_tags).SingleOrDefaultAsync(p => p.Id == id);
                    if (pp != null)
                    {
                        foreach(Tag tg in pp.Pizza_tags.ToList())
                        {
                            pp.Pizza_tags.Remove(tg);
                        }
                        await _context.SaveChangesAsync();
                    }
                    _context.Entry(pp).State = EntityState.Detached;

                    // adding new tags selected
                    pizza.Pizza_tags = new List<Tag>();
                    pizza.Pizza_tags.AddRange(_context.Tag.Where(x => Pizza_tags.Contains(x.Id)));
                    _context.Update(pizza);

                    // updating pizza_image
                    if (ImageFile != null)
                    {
                        PizzaImage pi = _context.PizzaImage.SingleOrDefault(pi => pi.PizzaId == id);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pi.ImageFile = ImageFile;
                            pi.ImageFile.CopyTo(ms);
                            pi.Image_content = ms.ToArray();
                        }
                    }
                    
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaExists(pizza.Id))
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
            return View(pizza);
        }

        

        // Get: Pizzas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var pizza = await _context.Pizza.FindAsync(id);
            _context.Pizza.Remove(pizza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizza.Any(e => e.Id == id);
        }
    }
}
