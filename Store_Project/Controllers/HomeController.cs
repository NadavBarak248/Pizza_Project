using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store_Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Store_Project.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        

        public IActionResult Index()
        {
            WebRequest request = WebRequest.Create("https://api.twitter.com/2/users/1417505386249261065/tweets?max_results=15");
            request.Headers.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAADN2RwEAAAAA99APpYwtvppWfW2duHjt8Ttu4eo%3DWsyqvizl9TMvIhFM10BraeoEw5gIbctWF12DqMUFvkdsgv92pH");
            using (System.IO.Stream s = request.GetResponse().GetResponseStream())
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                {
                    var jsonResponse = sr.ReadToEnd();
                    ViewData["Tweets"] = jsonResponse;
                }
            }
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
