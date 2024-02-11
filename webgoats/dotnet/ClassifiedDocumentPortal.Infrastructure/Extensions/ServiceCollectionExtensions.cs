using ClassifiedDocumentPortal.Domain.Entities;
using ClassifiedDocumentPortal.Domain.Interfaces.Repositories;
using ClassifiedDocumentPortal.Infrastructure.Data.Context;
using ClassifiedDocumentPortal.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClassifiedDocumentPortal.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ClassifiedDocumentPortalDbContext>(options =>
            {
                options.UseSqlite("Data Source=ClassifiedDocumentPortalDatabase.db");
            });

            services
                .AddDatabaseDeveloperPageExceptionFilter()
                .AddDefaultIdentity<PortalUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredUniqueChars = 0;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ClassifiedDocumentPortalDbContext>();

            services.AddScoped<IDocumentRepository, DocumentRepository>();

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ClassifiedDocumentPortalDbContext>();
                dbContext.Database.Migrate();
                ClassifiedDocumentPortalDbContextInitializer.Initialize(dbContext, serviceScope.ServiceProvider);
            }

            return app;
        }
    }
}
