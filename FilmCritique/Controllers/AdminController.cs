using FilmCritique.Entities.Model.Concrete;
using FilmCritique.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FilmCritique.BL.Managers.Abstract;

namespace FilmCritique.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMovieManager _movieManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(IMovieManager movieManager, UserManager<AppUser> userManager)
        {
            _movieManager = movieManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Admin sayfası için gerekli verileri al
            List<Movie> movies = await _movieManager.GetAllAsync();
            return View(movies);
        }
    }
}
