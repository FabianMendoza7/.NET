using CleanArch.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Mvc.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfiguration));
            Application.AutoMapper.AutoMapperConfiguration.RegisterMappings();
        }
    }
}
