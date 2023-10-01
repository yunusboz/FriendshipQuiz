using DataAccess.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Utility;

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
            }).AddEntityFrameworkStores<BaseDbContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
