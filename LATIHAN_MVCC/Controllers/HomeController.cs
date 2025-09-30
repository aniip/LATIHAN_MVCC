using System.Diagnostics;
using LATIHAN_MVCC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // ?? BARIS TAMBAHAN 1

namespace LATIHAN_MVCC.Controllers
{
    [Authorize] // ?? BARIS TAMBAHAN 2: Ini melindungi SEMUA Action di Controller ini
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Jika user belum login, secara otomatis akan dialihkan ke /Account/Login
            return View();
        }

        [AllowAnonymous] // Contoh: Jika Anda ingin Privacy bisa diakses tanpa login
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Widgets()
        {
            return View(); // Ini akan mencari Views/Home/Widgets.cshtml
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}