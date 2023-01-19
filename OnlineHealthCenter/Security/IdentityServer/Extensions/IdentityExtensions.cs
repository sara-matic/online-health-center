using IdentityServer.Data;
using IdentityServer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"));
            });

            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
