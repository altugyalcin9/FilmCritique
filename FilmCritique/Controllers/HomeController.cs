using FilmCritique.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


using FilmCritique.BL.Managers.Abstract;
using FilmCritique.Entities.Model.Concrete;

namespace FilmCritique.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieManager _movieManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IMovieManager movieManager)
        {
            _logger = logger;
            _movieManager = movieManager;
        }

        public async Task<IActionResult> Index()
        {
            List<Movie> movies = await _movieManager.GetAllAsync();
            ViewBag.Role = "Admin";
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
