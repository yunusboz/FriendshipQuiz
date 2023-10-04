using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Utility.Constants;
using WebApp.Models;

namespace WebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(Roles.Admin))
                {
                    return RedirectToAction("Index","Quiz", new {area = Roles.Admin});
                }
                else
                {
                    return RedirectToAction("Index", "User", new { area = "AppUser" });
                }
            }
            else
            {
                return View();
            }
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