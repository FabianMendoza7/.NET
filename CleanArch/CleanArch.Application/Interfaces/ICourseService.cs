using CleanArch.Application.ViewModels;
using System.Collections.Generic;

namespace CleanArch.Application.Interfaces
{
    public interface ICourseService
    {
        void Create(CourseViewModel courseViewModel);
        IEnumerable<CourseViewModel> GetCourses();
    }
}
