using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SkinetWebApi.Errors;
using System.Linq;

namespace SkinetWebApi.Extensions
{
    public static class ApplicationServicesExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // BADAH.Comment
            // In this line we inject ProductRepository to services of our project using AddScoped
            services.AddScoped<IProductRepository, ProductRepository>();

            // In this line we inject GenericRepository to services of our project using AddScoped
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

            // To be honest this section i didn't understand it
            // what i understood that we extended the baseclass and
            // we changed to anthoer behavior
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                    .Where(e => e.Value.Errors.Count > 0)
                                    .SelectMany(x => x.Value.Errors)
                                    .Select(x => x.ErrorMessage).ToArray();

                    var errorsResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorsResponse);
                };
            });

            return services;
        }
    }
}
