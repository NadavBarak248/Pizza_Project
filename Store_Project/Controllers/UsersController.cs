using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store_Project.Data;
using Store_Project.Models;

namespace Store_Project.Controllers
{
    public class UsersController : Controller
    {
        private readonly Store_ProjectContext _context;

        public UsersController(Store_ProjectContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult AccessDenied()  
        {
            return View();
        }


        // POST: Users/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                var q = _context.User.FirstOrDefault(u => u.Username == user.Username);

                if (q == null)
                {
                    user.Type = User_type.Client;
                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    var u = _context.User.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

                    Signin(u);

                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    ViewData["Error"] = "unable to comply; username already exist";
                }

            }
            return View(user);
        }

        //GET: Users/RegisterAdmin
        [Authorize(Roles = "Admin")]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        // POST: Users/RegisterAdmin
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin([Bind("Id,Username,Password,Type")] User user)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists
                var userExists = _context.User.FirstOrDefault(u => u.Username == user.Username);

                if (userExists == null)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    ViewData["Error"] = "unable to comply; username already exist";
                }
            }
            return View(user);
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                var q = from u in _context.User
                        where u.Username == user.Username && u.Password == user.Password
                        select u;


                if (q.Count() > 0)
                {
                   
                    Signin(q.First());
                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    ViewData["Error"] = "unable to comply; username /password are incorrect";
                }

            }
            return View(user);
        }

        private async void Signin(User account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, account.Type.ToString()),
            };

            var claimIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity),
                authProperties);
        }



        // GET: Users
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(string searchquery)
        {
            IOrderedQueryable<User> q = from u in _context.User
                                        where
                                        u.Username.Contains(searchquery) || (searchquery == null)
                                        orderby u.Id ascending
                                        select u;

            return Json(await q.ToListAsync());
        }

        // GET: Users/ChangePassword
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            return View("ChangePassword", await _context.User.FirstOrDefaultAsync(u => u.Username.Equals(this.User.Identity.Name)));
        }

        // POST: Users/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string oldpass, string newpass)
        {
            User curr_user = await _context.User.FirstOrDefaultAsync(u => u.Username.Equals(this.User.Identity.Name));
            curr_user.Password = newpass;
            _context.Update(curr_user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Home");
        }

        /*
        // GET: Users/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        */

        /*
        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Type")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        */

        private SelectList GetUserTypes()
        {
            var typeEnum = from User_type ut in Enum.GetValues(typeof(User_type))
                           select new
                           {
                               ID = (int)ut,
                               Name = ut.ToString()
                           };
            return new SelectList(typeEnum, "ID", "Name");

        }
        // GET: Users/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            ViewBag.UserTypes = GetUserTypes();
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Type")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if the username already exists
                    User userExists = _context.User.FirstOrDefault(u => (u.Username == user.Username && u.Id != user.Id));

                    if (userExists == null)
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewData["Error"] = "unable to comply; username already exist";
                        ViewBag.UserTypes = GetUserTypes();
                        return View(user);
                    }

                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(user);
        }

        // GET: Users/Delete/5
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            User curr_user = _context.User.FirstOrDefault(u => u.Username.Equals(this.User.Identity.Name));
            if (curr_user.Id == id)
            {
                ViewData["Error"] = "unable to comply; Can't delete current user";
                ViewBag.UserTypes = GetUserTypes();
                return View("Edit", curr_user);
            }
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

    }
}
