using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            var q = from o in _context.Order
                    join b in _context.Branch
                    on o.BranchId equals b.id
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


        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<int> pizzaIds, int branchid)
        {

            if (ModelState.IsValid)
            {
                // branch not found
                if (!_context.Branch.Any(e => e.id == branchid))
                    return NotFound();

                // no pizzas in order
                if (pizzaIds.Count == 0)
                    return NotFound();


                // building order
                Order order = new Order();

                order.User_order = _context.User.FirstOrDefault(u => u.Username.Equals(this.User.Identity.Name));
                order.Order_date = DateTime.Now;
                order.BranchId = branchid;
                order.Branch = _context.Branch.Find(branchid);
                order.Price = 0;

                // count instances of pizzas
                List<int> pizzas = new List<int>();
                List<int> instances = new List<int>();
                for (int i = 0; i < pizzaIds.Count; i++)
                {
                    if (!pizzas.Exists(pid => pid == pizzaIds[i]))
                    {
                        pizzas.Add(pizzaIds[i]);
                        instances.Add(1);
                    }
                    else
                    {
                        int instances_id = pizzas.IndexOf(pizzaIds[i]);
                        instances[instances_id] += 1;
                    }
                }

                // creating pizzas in order
                List<PizzasInOrder> pizzasOrder = new List<PizzasInOrder>();
                for (int i = 0; i < pizzas.Count; i++)
                {
                    // pizza not found in DB
                    if (!_context.Pizza.Any(e => e.Id == pizzas[i]))
                        return NotFound();

                    
                    PizzasInOrder po = new PizzasInOrder();
                    po.Pizzas = _context.Pizza.Find(pizzas[i]);

                    // pizza not in display mode
                    if (!po.Pizzas.To_present)
                        return NotFound();

                    
                   
                    po.PizzaId = pizzas[i];
                    po.Pizzas = _context.Pizza.Find(pizzas[i]);
                    po.Orders = order;
                    po.Quantity = instances[i];
                    pizzasOrder.Add(po);

                    order.Price += po.Pizzas.Price * instances[i];
                }
                order.PizzaOrder = pizzasOrder;


                _context.Add(order);
                await _context.SaveChangesAsync();
                
            }

            return Json(new { redirectToUrl = Url.Action("MyOrders", "Orders") });
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(o => o.User_order).Include(o => o.Branch).FirstOrDefaultAsync(e => e.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.users = new SelectList(_context.User, "Id", "Username", order.UserId);
            ViewBag.branches = new SelectList(_context.Branch, nameof(Branch.id), nameof(Branch.Branch_name), order.BranchId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Order_date,Price")] Order order, int User_order, int Branch)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            order.UserId = User_order;
            order.User_order = _context.User.Find(User_order);
            order.BranchId = Branch;
            order.Branch = _context.Branch.Find(Branch);

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

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Order.Include(o => o.User_order).FirstOrDefaultAsync(o => o.Id == id);
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
