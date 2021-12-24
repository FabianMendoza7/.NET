using CleanArch.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infra.Data.Context
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
    }
}
