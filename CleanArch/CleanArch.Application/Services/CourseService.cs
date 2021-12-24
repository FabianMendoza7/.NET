using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Commands;
using CleanArch.Domain.Core.Bus;
using CleanArch.Domain.Interfaces;
using System.Collections.Generic;

namespace CleanArch.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        private readonly IMediatorHandler mediatorHandler;
        private readonly IMapper mapper;

        public CourseService(ICourseRepository courseRepository,
            IMediatorHandler mediatorHandler,
            IMapper mapper)
        {
            this.courseRepository = courseRepository;
            this.mediatorHandler = mediatorHandler;
            this.mapper = mapper;
        }

        public void Create(CourseViewModel courseViewModel)
        {
            mediatorHandler.SendCommand(mapper.Map<CreateCourseCommand>(courseViewModel));
        }

        public IEnumerable<CourseViewModel> GetCourses()
        {
            return courseRepository.GetCourses().ProjectTo<CourseViewModel>(mapper.ConfigurationProvider);
        }
    }
}
