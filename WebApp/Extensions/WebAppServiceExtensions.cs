using DataAccess.Contexts;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Extensions
{
    public static class WebAppServiceExtensions
    {
        public static IServiceCollection AddWebAppServices(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser,IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<BaseDbContext>();

            return services;
        }
    }
}
