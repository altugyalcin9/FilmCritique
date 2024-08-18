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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { FirstName=model.FirstName,LastName=model.LastName , UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                  //  await _userManager.AddToRoleAsync(user, "User"); //son eklediklerim
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Eğer buraya geldiyseniz, formda bir hata var demektir. Formu tekrar gösteriyoruz.
            return View(model);
        }
        #region rol deneme
        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new AppUser
        //        {
        //            FirstName = model.FirstName,
        //            LastName = model.LastName,
        //            UserName = model.Email,
        //            Email = model.Email
        //        };
        //        var result = await _userManager.CreateAsync(user, model.Password);

        //        if (result.Succeeded)
        //        {
        //            // Kullanıcıyı varsayılan olarak "User" rolüne ata
        //            await _userManager.AddToRoleAsync(user, "User");
        //            await _signInManager.SignInAsync(user, isPersistent: false);
        //            return RedirectToAction("Index", "Home");
        //        }

        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }

        //    // Eğer buraya geldiyseniz, formda bir hata var demektir. Formu tekrar gösteriyoruz.
        //    return View(model);
        //}
        #endregion

        //son yaptıklarım
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


    }
}
