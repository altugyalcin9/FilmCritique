using FilmCritique.BL.Managers.Abstract;
using FilmCritique.DAL.Repository.Concrete;
using FilmCritique.Entities.DbContexts;
using FilmCritique.Entities.Model.Abstract;
using FilmCritique.Entities.Model.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmCritique.BL.Managers.Concrete
{
    public class MovieManager : ManagerBase<Movie>, IMovieManager
    {
        private readonly AppDbContext _context;

        public MovieManager(AppDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<UserReview>> GetCommentsByMovieIdAsync(int movieId)
        {
            return await _context.UserReviews
                                 .Where(review => review.MovieId == movieId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> SearchMoviesAsync(string query)
        {
            return await _context.Movies
                .Where(m => m.MovieName.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }



        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            /*var movie = await _context.Movies
            .Include(m => m.MovieDirectors)
            .Include(m => m.MovieActors)
            .Include(m => m.MovieCategories)
            .Include(m => m.UserReviews)
            .FirstOrDefaultAsync(m => m.Id == id);*/
            var movie = await _context.Movies
            .Include(m => m.MovieDirectors)
                .ThenInclude(md => md.Director)
            .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
            .Include(m => m.MovieCategories)
                .ThenInclude(mc => mc.Category)
            .Include(m => m.UserReviews)
            .FirstOrDefaultAsync(m => m.Id == id);

            return movie;
        }
    }
}