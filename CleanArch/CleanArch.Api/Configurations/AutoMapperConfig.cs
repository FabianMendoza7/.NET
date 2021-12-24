using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Api.Configurations
{
    public static class AutoMapperConfig
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            Application.AutoMapper.AutoMapperConfiguration.RegisterMappings();
            services.AddAutoMapper(typeof(Application.AutoMapper.AutoMapperConfiguration));
        }
    }
}
