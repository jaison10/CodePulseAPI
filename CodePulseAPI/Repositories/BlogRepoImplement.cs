using CodePulseAPI.Data;
using CodePulseAPI.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace CodePulseAPI.Repositories
{
    public class BlogRepoImplement
    {
        private readonly CodePulseDbContext context;

        public BlogRepoImplement(CodePulseDbContext context)
        {
            this.context = context;
        }
        
        public async Task<BlogPosts?>CreateBlog(BlogPosts newBlog)
        {
            var blog =  await this.context.BlogPosts.AddAsync(newBlog);
            if (blog == null) { return null; }
            return blog.Entity;
        }
        public async Task<IEnumerable<BlogPosts>> GetAllBlogs()
        {
            return await this.context.BlogPosts.ToListAsync();
        }
    }
}
