using CleanArch.Domain.Commands;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Domain.CommandHandlers
{
    public class CourseCommandHandler : IRequestHandler<CreateCourseCommand, bool>
    {
        private readonly ICourseRepository courseRepository;

        public CourseCommandHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public Task<bool> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Course
            {
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Name = request.Name
            };

            courseRepository.Add(course);

            return Task.FromResult(true);
        }
    }
}
