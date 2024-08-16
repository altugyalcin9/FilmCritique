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
    public class MovieActorManager:ManagerBase<MovieActor>,IMovieActorManager
    {
        private readonly AppDbContext _context;
        public MovieActorManager(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task UpdateMovieActorsAsync(int movieId, List<int> actorIds)
        {
            var existingActors = _context.MovieActors.Where(ma => ma.MovieId == movieId).ToList();

            // Mevcut iliþkileri sil
            _context.MovieActors.RemoveRange(existingActors);

            // Yeni aktör iliþkilerini ekle
            foreach (var actorId in actorIds)
            {
                _context.MovieActors.Add(new MovieActor
                {
                    MovieId = movieId,
                    ActorId = actorId
                });
            }

            await _context.SaveChangesAsync();
        }

    }
}
