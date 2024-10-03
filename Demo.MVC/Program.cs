using Demo.API.Extentions;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Data;
using Demo.DAL.Identity;
using Demo.MVC.Helpers;
using Demo.MVC.Services;
using Demo.MVC.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Demo.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
			builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
			builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));
			builder.Services.AddAutoMapper(typeof(MapProfiles));
			builder.Services.AddDbContext<MagicVillaDbContext>(options => {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
            builder.Services.AddDbContext<MagicVillaIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddIdentityServices(builder.Configuration);
            
            builder.Services.AddHttpClient<IVillaService, VillaService>();
            builder.Services.AddScoped<IVillaService, VillaService>();
            builder.Services.AddHttpClient<IVillaNumberSevices, VillaNumberServices>();
            builder.Services.AddScoped<IVillaNumberSevices, VillaNumberServices>();

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
