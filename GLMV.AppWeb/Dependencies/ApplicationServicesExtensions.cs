using GLMV.Application.Services;
using GLMV.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace GLMV.AppWeb.Dependencies
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Application Services
            services.AddScoped(typeof(RepositoryService<>));
            services.AddScoped<ProductService>();
            services.AddScoped<SalesPersonService>();
            services.AddScoped<CategoryService>();

            // Repositories
            services.AddScoped<SalesPersonRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<ProductRepository>();

            // Servkces
            services.AddRazorPages();
            services.AddSession();
      
        }
    }
}
