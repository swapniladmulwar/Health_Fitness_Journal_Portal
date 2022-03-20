using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Health.Models;
using Microsoft.AspNetCore.Authorization;

namespace Health.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public RedirectToActionResult Index()
        {
            var role = User.Claims.Single(c => c.Type == "role").Value;
            if ("admin".Equals(role, StringComparison.InvariantCultureIgnoreCase))
            {
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Index", "User");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
