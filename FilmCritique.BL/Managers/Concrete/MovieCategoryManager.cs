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
    public class MovieCategoryManager:ManagerBase<MovieCategory>,IMovieCategoryManager
    {
        private readonly AppDbContext _context;

        public MovieCategoryManager(AppDbContext context) : base(context)
        {
            _context = context;

        }
        public async Task UpdateMovieCategoriesAsync(int movieId, List<int> categoryIds)
        {
            var existingCategories = _context.MovieCategories.Where(mc => mc.MovieId == movieId).ToList();

            // Mevcut iliþkileri sil
            _context.MovieCategories.RemoveRange(existingCategories);

            // Yeni kategori iliþkilerini ekle
            foreach (var categoryId in categoryIds)
            {
                _context.MovieCategories.Add(new MovieCategory
                {
                    MovieId = movieId,
                    CategoryId = categoryId
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
