using FilmCritique.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FilmCritique.BL.Managers.Abstract;
using FilmCritique.Entities.Model.Concrete;
using Microsoft.AspNetCore.Identity; // Identity kütüphanesi ekleyin

namespace FilmCritique.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieManager _movieManager;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager; // UserManager ekleyin

        public HomeController(ILogger<HomeController> logger, IMovieManager movieManager, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _movieManager = movieManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<Movie> movies = await _movieManager.GetAllAsync();

            // Kullanýcýnýn rolünü al
            string userRole = "User"; // Varsayýlan rol

            var user = await _userManager.GetUserAsync(User); // Þu anki kullanýcýyý al
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user); // Kullanýcýnýn rollerini al
                if (roles.Any())
                {
                    userRole = roles.First(); // Ýlk rolü al (Varsayýlan olarak)
                }
            }

            ViewBag.Role = userRole; // Rolü ViewBag'e ekle

            return View(movies);
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
