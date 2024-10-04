using Demo.DAL.Entities.Identity;
using Demo.DAL.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Demo.API.Extentions
{
    public static class ApplicationIdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services , IConfiguration configuration)
        {

            services.AddIdentity<AppUser, IdentityRole>(options => {
                //options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
            }).AddEntityFrameworkStores<MagicVillaIdentityDbContext>();

            var key = configuration.GetValue<string>("ApiSettings:Secret");

            services.AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme*/options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options => {
                 //options.TokenValidationParameters = new TokenValidationParameters()
                 //{
                 //    ValidateIssuer = true,
                 //    ValidIssuer = configuration["JWT:ValidIssuer"],
                 //    ValidateAudience = true,
                 //    ValidAudience = configuration["JWT:ValidAudience"],
                 //    ValidateLifetime = true,
                 //    ValidateIssuerSigningKey = true,
                 //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                 //};
             });
            services.AddAuthorization();

            return services;
        }
    }
}
