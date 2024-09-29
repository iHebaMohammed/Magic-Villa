
using Demo.API.Extentions;
using Demo.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Demo.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel
            //    .Debug()
            //    .WriteTo
            //    .File("Log/VillaLogs.txt", rollingInterval: RollingInterval.Day)
            //    .CreateLogger();
            //builder.Host.UseSerilog();



            // Add services to the container.
            builder.Services.AddDbContext<MagicVillaDbContext>(options => 
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddApplicationServices();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // DataSeed:
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = serviceProvider.GetRequiredService<MagicVillaDbContext>();
                    await context.Database.MigrateAsync(); // Will update database automaticlly

                    await MagicVillaContextSeed.SeedAsync(context, loggerFactory);
                }
                catch (Exception ex) 
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message);
                }
            }


            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
