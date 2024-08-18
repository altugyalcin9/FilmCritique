using FilmCritique.BL.Managers.Abstract;
using FilmCritique.BL.Managers.Concrete;
using FilmCritique.Entities.DbContexts;
using FilmCritique.Entities.Model.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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

            // DI ile baðýmlýlýklarý ekleyin
            builder.Services.AddScoped<IMovieManager, MovieManager>();
            builder.Services.AddScoped<IActorManager, ActorManager>();
            builder.Services.AddScoped<IMovieActorManager, MovieActorManager>();
            builder.Services.AddScoped<IDirectorManager, DirectorManager>();
            builder.Services.AddScoped<IMovieDirectorManager, MovieDirectorManager>();
            builder.Services.AddScoped<ICategoryManager, CategoryManager>();
            builder.Services.AddScoped<IMovieCategoryManager, MovieCategoryManager>();

            var app = builder.Build();

            // Role setup
            CreateRolesAndUsers(app).Wait();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Identity için Authentication middleware'i ekleyin
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static async Task CreateRolesAndUsers(WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                string[] roleNames = { "Admin", "User" };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                // Create a default admin user
                var adminUser = await userManager.FindByEmailAsync("admin@filmcritique.com");
                if (adminUser == null)
                {
                    adminUser = new AppUser()
                    {
                        UserName = "admin",
                        Email = "admin@filmcritique.com",
                        FirstName = "Admin",
                        LastName = "User"
                    };
                    await userManager.CreateAsync(adminUser, "Admin@123");
                }

                // Add admin user to Admin role
                if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                // Create a default user
                var defaultUser = await userManager.FindByEmailAsync("user@filmcritique.com");
                if (defaultUser == null)
                {
                    defaultUser = new AppUser()
                    {
                        UserName = "user",
                        Email = "user@filmcritique.com",
                        FirstName = "Default",
                        LastName = "User"
                    };
                    await userManager.CreateAsync(defaultUser, "User@123");
                }

                // Add default user to User role
                if (!await userManager.IsInRoleAsync(defaultUser, "User"))
                {
                    await userManager.AddToRoleAsync(defaultUser, "User");
                }
            }
        }
    }
}
