namespace CleanArch.Domain.Commands
{
    public class CreateCourseCommand : CourseCommand
    {
        public CreateCourseCommand(string name, string desc, string imageUrl)
        {
            Name = name;
            Description = desc;
            ImageUrl = imageUrl;
        }
    }
}
