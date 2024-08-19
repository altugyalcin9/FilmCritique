using FilmCritique.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

//using FilmCritique.Models;

using FilmCritique.BL.Managers.Abstract;
using FilmCritique.Entities.Model.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using FilmCritique.Model.ViewModels;
using Microsoft.EntityFrameworkCore;
using FilmCritique.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using FilmCritique.Entities.DbContexts;
using FilmCritique.ViewModels;

namespace FilmCritique.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMovieManager _movieManager;
        private readonly IMovieActorManager _movieActorManager;
        private readonly IActorManager _actorManager;
        private readonly IDirectorManager _directorManager;

        private readonly ICategoryManager _categoryManager;

        private readonly IWebHostEnvironment _webHostEnvironment;



        private readonly IMovieDirectorManager _movieDirectorManager;
        private readonly IMovieCategoryManager _movieCategoryManager;

        public MovieController(IWebHostEnvironment webHostEnvironment, IMovieManager movieManager, IMovieActorManager movieActorManager,
         IMovieDirectorManager movieDirectorManager, IMovieCategoryManager movieCategoryManager,
         IActorManager actorManager, IDirectorManager directorManager, ICategoryManager categoryManager,AppDbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _movieManager = movieManager;
            _movieActorManager = movieActorManager;
            _movieDirectorManager = movieDirectorManager;
            _movieCategoryManager = movieCategoryManager;
            _actorManager = actorManager;
            _directorManager = directorManager;
            _categoryManager = categoryManager;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }
        public async Task<IActionResult> Create()
        {
            var model = new MovieViewModel();

            ViewData["Actors"] = new SelectList(await _actorManager.GetAllAsync(), "Id", "FullName");
            ViewData["Directors"] = new SelectList(await _directorManager.GetAllAsync(), "Id", "FullName");
            ViewData["Categories"] = new SelectList(await _categoryManager.GetAllAsync(), "Id", "Name");

            return View(model);
        }


        // POST: /Movies/Create
        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    MovieName = model.MovieName,
                    ReleaseDate = model.ReleaseDate,
                    TeaserUrl = model.TeaserUrl,
                    Duration = model.Duration,
                    Description = model.Description,
                    AdminRating = model.AdminRating
                };
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.PhotoUrl.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images/moviePhotos");
                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    await model.PhotoUrl.CopyToAsync(fileStream);
                }
                movie.PhotoUrl = fileName;



                await _movieManager.InsertAsync(movie);
                // Add the movie to the database


                // Adding MovieActor relationships
                if (model.SelectedActorIds != null && model.SelectedActorIds.Any())
                {
                    foreach (var actorId in model.SelectedActorIds)
                    {
                        var movieActor = new MovieActor
                        {
                            MovieId = movie.Id,
                            ActorId = actorId
                        };

                        await _movieActorManager.InsertAsync(movieActor);
                    }
                }

                // Adding MovieDirector relationships
                if (model.SelectedDirectorIds != null && model.SelectedDirectorIds.Any())
                {
                    foreach (var directorId in model.SelectedDirectorIds)
                    {
                        var movieDirector = new MovieDirector
                        {
                            MovieId = movie.Id,
                            DirectorId = directorId
                        };

                        await _movieDirectorManager.InsertAsync(movieDirector);
                    }
                }

                // Adding MovieCategory relationships
                if (model.SelectedCategoryIds != null && model.SelectedCategoryIds.Any())
                {
                    foreach (var categoryId in model.SelectedCategoryIds)
                    {
                        var movieCategory = new MovieCategory
                        {
                            MovieId = movie.Id,
                            CategoryId = categoryId
                        };

                        await _movieCategoryManager.InsertAsync(movieCategory);
                    }
                }

                return RedirectToAction("Index", "Home");
            }





            // If we got this far, something failed, redisplay form
            ViewData["Actors"] = new SelectList(await _actorManager.GetAllAsync(), "Id", "FullName");
            ViewData["Directors"] = new SelectList(await _directorManager.GetAllAsync(), "Id", "FullName");
            ViewData["Categories"] = new SelectList(await _categoryManager.GetAllAsync(), "Id", "Name"); return View(model);
        }


        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieManager.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieManager.GetMovieByIdAsync(id);
            if (movie != null)
            {
                _movieManager.DeleteAsync(movie);
                if (movie.PhotoUrl != null)
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, $"images\\moviePhotos\\{movie.PhotoUrl}");
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieManager.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var model = new MovieEditViewModel
            {
                Id = movie.Id,
                MovieName = movie.MovieName,
                ReleaseDate = movie.ReleaseDate,
                TeaserUrl = movie.TeaserUrl,
                Duration = movie.Duration,
                Description = movie.Description,
                AdminRating = movie.AdminRating,
                ExistingPhoto = movie.PhotoUrl,
                SelectedActorIds = movie.MovieActors.Select(ma => ma.ActorId).ToList(),
                SelectedDirectorIds = movie.MovieDirectors.Select(md => md.DirectorId).ToList(),
                SelectedCategoryIds = movie.MovieCategories.Select(mc => mc.CategoryId).ToList()
            };

            ViewData["Actors"] = new SelectList(await _actorManager.GetAllAsync(), "Id", "FullName");
            ViewData["Directors"] = new SelectList(await _directorManager.GetAllAsync(), "Id", "FullName");
            ViewData["Categories"] = new SelectList(await _categoryManager.GetAllAsync(), "Id", "Name");


            return View(model);
        }

        //// GET: Movie/Search
        //[HttpGet]
        //public async Task<IActionResult> Search(string query)
        //{
        //    if (string.IsNullOrWhiteSpace(query))
        //    {
        //        return View("Index", await _movieManager.GetAllMoviesAsync());
        //    }

        //    var movies = await _movieManager.SearchMoviesAsync(query);
        //    return View("Index", movies);
        //}





        // MovieController.cs
        [HttpGet]
        public async Task<IActionResult> Comment([FromRoute] int id)
        {
            var movie = await _movieManager.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            ViewBag.movie = "movie";
            var comments = await _movieManager.GetCommentsByMovieIdAsync(id);
            ViewBag.Comments = comments;

            ViewData["Actors"] = new SelectList(await _actorManager.GetAllAsync(), "Id", "FullName");
            ViewData["Directors"] = new SelectList(await _directorManager.GetAllAsync(), "Id", "FullName");
            ViewData["Categories"] = new SelectList(await _categoryManager.GetAllAsync(), "Id", "Name");

            return View(movie);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(int id, CreateReviewViewModel model)
        {
            // Giriş yapmamış kullanıcıların yorum yapmasını engelle
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Warning"] = "You must be logged in to comment.";
                return RedirectToAction("Comment", new { id = id });
            }

            // Model doğrulama
            if (!ModelState.IsValid)
            {
                // Yorum oluşturulamadıysa, mevcut filmi ve yorumları tekrar göster
                var movie = await _context.Movies
                    .Include(m => m.MovieActors)
                    .Include(m => m.MovieDirectors)
                    .Include(m => m.MovieCategories)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (movie == null)
                {
                    return NotFound();
                }

                ViewBag.movie = "movie";
                var comments = await _context.UserReviews
                    .Where(r => r.MovieId == id)
                    .Select(r => new { r.Comment, r.UserId }) // UserId ekle
                    .ToListAsync();

                ViewBag.Comments = comments;
                ViewData["Actors"] = new SelectList(await _context.Actors.ToListAsync(), "Id", "FullName");
                ViewData["Directors"] = new SelectList(await _context.Directors.ToListAsync(), "Id", "FullName");
                ViewData["Categories"] = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");

                return View("Comment", movie);
            }

            // Yorum ekleme işlemi
            var userId = User.Identity.Name; // Şu anki kullanıcının ID'si
            var review = new UserReview
            {
                Comment = model.Comment,
                Rating = model.Rating,
                MovieId = id,
                UserId = userId // Kullanıcı ID'sini ekle
            };

            _context.UserReviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Comment", new { id = id });
        }






        // POST: Movie/Edit/5
        [HttpPost]

        public async Task<IActionResult> Edit(MovieEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = await _movieManager.GetByIdAsync(model.Id);

                if (movie == null)
                {
                    return NotFound();
                }

                // Güncellenen alanlarý kontrol edin
                if (model.MovieName != null)
                {
                    movie.MovieName = model.MovieName;
                }

                if (model.ReleaseDate.HasValue)
                {
                    movie.ReleaseDate = model.ReleaseDate.Value;
                }

                if (model.Duration.HasValue)
                {
                    movie.Duration = model.Duration.Value;
                }

                if (model.Description != null)
                {
                    movie.Description = model.Description;
                }

                if (model.TeaserUrl != null)
                {
                    movie.TeaserUrl = model.TeaserUrl;
                }

                if (model.AdminRating.HasValue)
                {
                    movie.AdminRating = model.AdminRating.Value;
                }

                // Fotoðraf güncellemesi
                if (model.PhotoUrl != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.PhotoUrl.FileName);
                    string productPath = Path.Combine(_webHostEnvironment.WebRootPath, @"images/moviePhotos");
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        await model.PhotoUrl.CopyToAsync(fileStream);
                    }
                    movie.PhotoUrl = fileName;
                }

                await _movieManager.UpdateAsync(movie);

                // Aktör, Yönetmen, Kategori iliþkilerini güncelleyin
                await _movieActorManager.UpdateMovieActorsAsync(movie.Id, model.SelectedActorIds ?? new List<int>());
                await _movieDirectorManager.UpdateMovieDirectorsAsync(movie.Id, model.SelectedDirectorIds ?? new List<int>());
                await _movieCategoryManager.UpdateMovieCategoriesAsync(movie.Id, model.SelectedCategoryIds ?? new List<int>());

                return RedirectToAction("Index", "Home");
            }

            // Eðer bir hata varsa, formu tekrar görüntüle
            ViewData["Actors"] = new SelectList(await _actorManager.GetAllAsync(), "Id", "FullName");
            ViewData["Directors"] = new SelectList(await _directorManager.GetAllAsync(), "Id", "FullName");
            ViewData["Categories"] = new SelectList(await _categoryManager.GetAllAsync(), "Id", "Name");

            return View(model);

        }

        //      public async Task<IActionResult> Index(int? pageNumber)
        //{
        //    int pageSize = 10; // Sayfa baþýna gösterilecek öðe sayýsý
        //    var movies = _movieManager.GetAllMovies() // GetAllMovies() yerine uygun bir metot adý kullanýn
        //        .Include(m => m.MovieActors)
        //        .Include(m => m.MovieDirectors)
        //        .AsNoTracking(); // Ýzlemeyi kapatýr, performansý artýrabilir
        //    var pagedMovies = await PaginatedList<Movie>.CreateAsync(movies, pageNumber ?? 1, pageSize);
        //    return View(pagedMovies);
        //}






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}