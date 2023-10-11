using CodePulseAPI.Data;
using CodePulseAPI.Models.DomainModels;
using DTO = CodePulseAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace CodePulseAPI.Repositories
{
    public class BlogRepoImplement : IBlogRepository
    {
        private readonly CodePulseDbContext context;
        private readonly ICategoryRepository categoryRepo;

        public BlogRepoImplement(CodePulseDbContext context, ICategoryRepository categoryRepo)
        {
            this.context = context;
            this.categoryRepo = categoryRepo;
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
                .Include(x=> x.Categories)
                .ToListAsync();
        }
        public async Task<BlogPosts?> GetABlog(Guid blogId)
        {
            //return await this.context.BlogPosts.FirstOrDefaultAsync(blog => blog.Id == blogId);
            //return await this.context.BlogPosts.SingleOrDefaultAsync(blog => blog.Id == blogId);
            return await this.context.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(blog => blog.Id == blogId);
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

                existingBlog.Categories = new List<Category>();
                foreach (var categoryGuid in blog.CategoryIDs)
                {
                    var existingCategory = await this.categoryRepo.GetCategoryDetails(categoryGuid);
                    if (existingCategory != null)
                    {
                        existingBlog.Categories.Add(existingCategory);
                    }
                }
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
        public async Task<BlogPosts?> GetBlogByUrl(String urlHandle)
        {
            var blog = await this.context.BlogPosts.FirstOrDefaultAsync(blog => blog.UrlHandle == urlHandle);
            if (blog == null) return null;
            return blog;
        }
    }
}
