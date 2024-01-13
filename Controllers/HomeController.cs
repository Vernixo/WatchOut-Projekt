using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WatchOut.Areas.Identity.Data;
using WatchOut.Models;

namespace WatchOut.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<WatchOutUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<WatchOutUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }
        public IActionResult Cart()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var isAdmin = user.IsAdmin;
                // Teraz mo¿esz u¿yæ isAdmin w logice widoku lub przekazaæ do widoku
                ViewData["IsAdmin"] = isAdmin;
            }

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Watches()
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
