using FilmCritique.Entities.Model.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace FilmCritique.Entities.DbContexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Movie> Movies { get; set; }
        //public DbSet<UserReview> UserReviews { get; set; }

        public DbSet<MovieActor> MovieActors { get; set; }

        public DbSet<MovieCategory> MovieCategories { get; set; }

        public DbSet<MovieDirector> MovieDirectors { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext()
        {
        }
        // Define DbSets for your entities

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = "Server=127.0.0.1;Database=FilmCritique;Uid=root;Pwd=Password2002;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Fluent API configurations go here
            // For example:
            // modelBuilder.Entity<Movie>().HasKey(m => m.MovieId);
        }
    }
}
