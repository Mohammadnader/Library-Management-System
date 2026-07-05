using Library.Data;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configure Entity Framework with SQLite
            builder.Services.AddDbContext<LibraryContext>(options => 
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add session support for user authentication
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

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
            app.UseSession(); // Enable session middleware
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Welcome}/{id?}");

            // Apply migrations and seed data
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
                context.Database.Migrate(); // Apply any pending migrations
            }

            app.Run();
        }
    }
}
