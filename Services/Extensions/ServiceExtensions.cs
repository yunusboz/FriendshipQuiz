using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServiceServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IQuizService, QuizManager>();
            services.AddScoped<IQuestionService, QuestionManager>();
            services.AddScoped<IQuizResultService, QuizResultManager>();
            services.AddScoped<IApplicationUserService, ApplicationUserManager>();

            return services;
        }
    }
}
