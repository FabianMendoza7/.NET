using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Arch.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterService(IServiceCollection services)
        {
            // Application Layer.
            services.AddScoped<ICourseService, CourseService>();

            // Infra.Data Layer.
            services.AddScoped<ICourseRepository, CourseRepository>();
        }
    }
}
