using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SkinetWebApi.Helpers;
using SkinetWebApi.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkinetWebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddControllers();

            //This line for connection string service.
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));

            // BADAH.Comment
            // In this line we inject ProductRepository to services of our project using AddScoped
            services.AddScoped<IProductRepository, ProductRepository>();

            // In this line we inject GenericRepository to services of our project using AddScoped
            services.AddScoped(typeof(IGenericRepository<>),(typeof(GenericRepository<>)));

            // Here we inject the auto mapper to the container services of dependency injection
            services.AddAutoMapper(typeof(MappingProfiles));



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                // We stop this middleware exception to use our custom error exception see line before if bloc
                //app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");


            app.UseHttpsRedirection();

            app.UseRouting();

            //This Line for implementing middleware of exploring static files
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
