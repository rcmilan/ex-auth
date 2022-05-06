using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class ModuleDependency
    {
        public static IServiceCollection AddDatabaseModule(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(config => config.UseInMemoryDatabase("memorydb"))
                .AddIdentity<IdentityUser, IdentityRole>(config =>
                {
                    config.Password.RequiredLength = 8;
                    config.Password.RequireDigit = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}