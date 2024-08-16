using FilmCritique.Entities.DbContexts;
using FilmCritique.Entities.Model.Concrete;
using FilmCritique.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCritique.Models;
using Microsoft.AspNetCore.Identity;

namespace FilmCritique.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Geçersiz giriş denemesi.");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_context.AppUsers.Any(c => c.Email == model.Email))
                {
                    var appuser = new AppUser
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,                       
                        Email = model.Email,
                      
                    };

                    _context.AppUsers.Add(appuser);
                    _context.SaveChanges();

                    return RedirectToAction("Login", "Account");
                }
                ModelState.AddModelError("Email", "This Email is already registered.");
            }

            return View(model);
        }

    }
}
