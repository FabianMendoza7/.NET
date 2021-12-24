using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Domain.CommandHandlers;
using CleanArch.Domain.Commands;
using CleanArch.Domain.Core.Bus;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Bus;
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.Data.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infra.Ioc
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<IRequestHandler<CreateCourseCommand, bool>, CourseCommandHandler>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<MainDbContext>();
        }
    }
}
