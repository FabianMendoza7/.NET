using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using CleanArch.Infra.Data.Context;
using System.Linq;

namespace CleanArch.Infra.Data.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly MainDbContext context;

        public CourseRepository(MainDbContext context)
        {
            this.context = context;
        }

        public void Add(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public IQueryable<Course> GetCourses()
        {
            return context.Courses;
        }
    }
}
