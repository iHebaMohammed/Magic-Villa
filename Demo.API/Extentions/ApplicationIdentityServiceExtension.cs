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
            var key = configuration.GetValue<string>("ApiSettings:Secret");


            services.AddIdentity<AppUser, IdentityRole>(options => 
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddEntityFrameworkStores<MagicVillaIdentityDbContext>()
                .AddDefaultTokenProviders();


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x => {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}
