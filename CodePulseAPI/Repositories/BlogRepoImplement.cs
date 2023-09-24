using CodePulseAPI.Data;
using CodePulseAPI.Models.DomainModels;
using DTO = CodePulseAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CodePulseAPI.Repositories
{
    public class BlogRepoImplement : IBlogRepository
    {
        private readonly CodePulseDbContext context;

        public BlogRepoImplement(CodePulseDbContext context)
        {
            this.context = context;
        }
        
        public async Task<BlogPosts?>CreateBlog(BlogPosts newBlog)
        {
            var blog =  await this.context.BlogPosts.AddAsync(newBlog);
            await this.context.SaveChangesAsync();
            if (blog == null) { return null; }
            return blog.Entity;
        }
        public async Task<IEnumerable<BlogPosts?>> GetAllBlogs(int page)
        {
            var loadPages = 10;
            return await this.context.BlogPosts.OrderByDescending(blog => blog.PublishedDate)
                .Skip((page-1) * loadPages)
                .Take(loadPages)
                .ToListAsync();
        }
        public async Task<BlogPosts?> GetABlog(Guid blogId)
        {
            return await this.context.BlogPosts.FirstOrDefaultAsync( blog=> blog.Id == blogId);
        }
        public async Task<BlogPosts?> UpdateBlog(Guid blogId, DTO.UpdateBlog blog)
        {
            var existingBlog = await this.GetABlog(blogId);
            if(existingBlog != null)
            {
                existingBlog.Title = blog.Title;
                existingBlog.ShortDesc = blog.ShortDesc;
                existingBlog.Content = blog.Content;
                existingBlog.FeaturedImgURL = blog.FeaturedImgURL;
                existingBlog.Author = blog.Author;
                existingBlog.IsVisible = blog.IsVisible;

                await this.context.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }
        public async Task<BlogPosts?> DeleteBlog(Guid blogId)
        {
            var blog = await this.GetABlog(blogId);
            if(blog != null)
            {
                this.context.BlogPosts.Remove(blog);
                await this.context.SaveChangesAsync();
                return blog;
            }
            return null;
        }
    }
}
