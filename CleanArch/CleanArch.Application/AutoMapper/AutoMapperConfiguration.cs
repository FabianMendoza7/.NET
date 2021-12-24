using AutoMapper;

namespace CleanArch.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(x =>
            {
                x.AddProfile(new ViewModelToDomainProfile());
                x.AddProfile(new DomainToViewModelProfile());
            });
        }
    }
}
