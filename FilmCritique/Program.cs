using FilmCritique.BL.Managers.Abstract;
using FilmCritique.BL.Managers.Concrete;
using FilmCritique.Entities.DbContexts;
using FilmCritique.Entities.Model.Concrete; // AppUser s�n�f�n� ekledi�inizden emin olun
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // Identity i�in gerekli k�t�phane

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

            // Identity hizmetlerini ekleyin
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // DI ile ba��ml�l�klar� ekleyin
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Identity i�in Authentication middleware'i ekleyin
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
