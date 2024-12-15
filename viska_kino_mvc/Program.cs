using Application.Interfaces;
using Application.Bussiness;
using Application.Services;
using Data_Access.Interfaces;
using Data_Access.Repositories;
using Data_Access.Database;
namespace viska_kino_mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddSingleton<IDatabaseAccess>(new DatabaseAccess(connectionString));

            builder.Services.AddSingleton<ILoginService, LoginService>();
            builder.Services.AddScoped<IScreeningService, ScreeningService>();
            builder.Services.AddScoped<IReservationService,ReservationService>();

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IScreeningRepository, ScreeningRepository>();
            builder.Services.AddTransient<IReservationRepository, ReservationRepository>();
            builder.Services.AddTransient<IScreeningRoomRepository, ScreeningRoomRepository>();
            builder.Services.AddTransient<IFilmRepository, FilmRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
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
