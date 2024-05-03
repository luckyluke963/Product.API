using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Product.API.Error;
using System.Reflection;

namespace Product.API.Extensions
{
    public static class APIRequestration
    {
        public static IServiceCollection AddAPIReguestration(this IServiceCollection services)
        {
            //AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //FileProvide
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(
                           Directory.GetCurrentDirectory(), "wwwroot"
                           )));

            services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = context =>
                {
                    var errorReposne = new ApiValidationErrorRespnse
                    {
                        Errors = context.ModelState.Where(x=>x.Value.Errors.Count > 0).SelectMany(x =>x.Value.Errors).Select(x =>x.ErrorMessage).ToArray()
                    };
                    return new BadRequestObjectResult(errorReposne);
                };
            });

            //ket noi api angluar
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", pol =>
                {
                    pol.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                });
            });

            return services;
        }
    }
}
