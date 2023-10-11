using CodePulseAPI.Models.DomainModels;
using DTO = CodePulseAPI.Models.DTO;

namespace CodePulseAPI.Repositories
{
    public interface IBlogRepository
    {
        Task<BlogPosts?> CreateBlog(BlogPosts newBlog);
        Task<IEnumerable<BlogPosts?>> GetAllBlogs(int page);
        Task<BlogPosts?> GetABlog(Guid blogId);
        Task<BlogPosts?> UpdateBlog(Guid blogId, DTO.UpdateBlog blog);
        Task<BlogPosts?> DeleteBlog(Guid blogId);
        Task<BlogPosts?> GetBlogByUrl(String urlHandle);
    }
}
