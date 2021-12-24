using AutoMapper;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Commands;

namespace CleanArch.Application.AutoMapper
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<CourseViewModel, CreateCourseCommand>()
                .ConstructUsing(x => new CreateCourseCommand(x.Name, x.Description, x.ImageUrl));
        }
    }
}
