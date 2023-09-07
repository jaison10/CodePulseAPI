using CodePulseAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CodePulseAPI.Data
{
    public class CodePulseDbContext : DbContext
    {
        public CodePulseDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogPosts> BlogPosts { get; set; }
    }
}
