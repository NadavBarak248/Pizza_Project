using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store_Project.Data;
using Store_Project.Models;

namespace Store_Project.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly Store_ProjectContext _context;

        public OrdersController(Store_ProjectContext context)
        {
            _context = context;
        }

        // GET: Orders
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            IOrderedQueryable<Order> q = from o in _context.Order.Include(o => o.User_order).Include(o => o.Branch).Include(o => o.PizzaOrder).ThenInclude(p => p.Pizzas)
                                         orderby o.Id ascending
                                         select o;

            return View(await q.ToListAsync());
        }

        // GET: Orders/MyOrders
        public async Task<IActionResult> MyOrders()
        {
            string current_user = this.User.Identity.Name;
            IOrderedQueryable<Order> q = from o in _context.Order.Include(o => o.User_order).Include(o => o.Branch).Include(o => o.PizzaOrder).ThenInclude(p => p.Pizzas)
                                         where o.User_order.Username == current_user
                                         orderby o.Id ascending
                                         select o;

            return View(await q.ToListAsync());
        }

        // GET: UsersOrders
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UsersOrders()
        {
            var q = from o in _context.Order.Include(o => o.User_order)
                    group o by o.User_order.Username into g
                    orderby g.Count() descending

                    select new
                    {
                        Username = g.Key,
                        Orders = g.Count()
                    };

            var result = await q.ToListAsync();

            ViewBag.users = new List<string>();
            ViewBag.counts = new List<int>();
            for (int i = 0; i < result.Count; i++)
            {
                ViewBag.users.Add(result[i].Username);
                ViewBag.counts.Add(result[i].Orders);
            }
            return View(result);
        }

        // GET: BranchesOrders
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BranchesOrders()
        {
            var q = from o in _context.Order.Include(o => o.Branch)
                    group o by o.Branch.Branch_name into g
                    orderby g.Count() descending

                    select new
                    {
                        Branchname = g.Key,
                        Orders = g.Count()
                    };

            var result = await q.ToListAsync();

            ViewBag.branches = new List<string>();
            ViewBag.counts = new List<int>();
            for (int i = 0; i < result.Count; i++)
            {
                ViewBag.branches.Add(result[i].Branchname);
                ViewBag.counts.Add(result[i].Orders);
            }
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(string user, string branch, double pricelimit, DateTime endingdate)
        {
           
            IOrderedQueryable < Order > q = from o in _context.Order.Include(o => o.User_order).Include(o => o.Branch).Include(o => o.PizzaOrder).ThenInclude(p => p.Pizzas)
                                            where
                                         (o.User_order.Username.Contains(user) || (user == null)) &&
                                         (o.Branch.Branch_name.Contains(branch) || (branch == null)) &&
                                         ((pricelimit >= o.Price) || (pricelimit <= 0)) &&
                                         (o.Order_date <= endingdate) 

                                         orderby o.Id ascending
                                         select o;

            return Json(await q.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            var q = from o in _context.Order.Include(o => o.PizzaOrder).ThenInclude(p => p.Pizzas)
                    join b in _context.Branch
                    on o.BranchId equals b.id
                    join u in _context.User
                    on o.UserId equals u.Id
                    
                    select new
                    {
                        Id = o.Id,
                        OrderDate = o.Order_date,
                        Price = o.Price,
                        Username = u.Username,
                        Branchname = b.Branch_name,
                        Pizzas = o.PizzaOrder
                    };
            var order = await q.FirstOrDefaultAsync(m => m.Id == id);

            string current_user = this.User.Identity.Name;
            if (!order.Username.Equals(current_user) && !this.User.IsInRole("Admin"))
            {
                return NotFound();
            }
            ViewBag.OrderDate = order.OrderDate;
            ViewBag.Price = order.Price;
            ViewBag.Id = order.Id;
            ViewBag.Username = order.Username;
            ViewBag.Branchname = order.Branchname;
            ViewBag.Pizzas = order.Pizzas;

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Order_date,Price")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Order_date,Price")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Order.Include(o => o.User_order).FirstOrDefaultAsync(o => o.Id == id);
            string current_user = this.User.Identity.Name;
            if (!order.User_order.Username.Equals(current_user) || !this.User.IsInRole("Admin"))
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
