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
    public class MovieDirectorManager:ManagerBase<MovieDirector>,IMovieDirectorManager
    {
        private readonly AppDbContext _context;

        public MovieDirectorManager(AppDbContext context) : base(context)
        {
            _context = context;

        }
        public async Task UpdateMovieDirectorsAsync(int movieId, List<int> directorIds)
        {
            var existingDirectors = _context.MovieDirectors.Where(md => md.MovieId == movieId).ToList();

            // Mevcut iliþkileri sil
            _context.MovieDirectors.RemoveRange(existingDirectors);

            // Yeni yönetmen iliþkilerini ekle
            foreach (var directorId in directorIds)
            {
                _context.MovieDirectors.Add(new MovieDirector
                {
                    MovieId = movieId,
                    DirectorId = directorId
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
