using FilmCritique.BL.Managers.Abstract;
using FilmCritique.BL.Managers.Concrete;
using FilmCritique.Entities.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FilmCritique
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var config = builder.Configuration;
                var connectionString = "Server=127.0.0.1;Database=FilmCritique;Uid=root;Pwd=Password2002;";
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            builder.Services.AddScoped<IMovieManager, MovieManager>();


            builder.Services.AddScoped<IActorManager, ActorManager>();
            builder.Services.AddScoped<IMovieActorManager, MovieActorManager>();



            builder.Services.AddScoped<IDirectorManager, DirectorManager>();
            builder.Services.AddScoped<IMovieDirectorManager, MovieDirectorManager>();



            builder.Services.AddScoped<ICategoryManager, CategoryManager>();
            builder.Services.AddScoped<IMovieCategoryManager, MovieCategoryManager>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
