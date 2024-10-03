using Demo.API.Errors;
using Demo.API.Helper;
using Demo.API.Services;
using Demo.API.Services.IServices;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IGenaricRepository<>) , typeof(GenaricRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(MappingProfiles));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Count() > 0)
                                                         .SelectMany(M => M.Value.Errors)
                                                         .Select(E => E.ErrorMessage)
                                                         .ToArray();
                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });


            return services;
        }
    }
}
