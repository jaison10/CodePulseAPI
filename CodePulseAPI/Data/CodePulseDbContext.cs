using CodePulseAPI.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CodePulseAPI.Data
{
    public class CodePulseDbContext : DbContext
    {
        public CodePulseDbContext(DbContextOptions<CodePulseDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<BlogPosts> BlogPosts { get; set; }
    }
}
