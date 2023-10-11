using AutoMapper;
using CodePulseAPI.Models.DomainModels;
using DTO = CodePulseAPI.Models.DTO;
using CodePulseAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodePulseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : Controller
    {
        private readonly IBlogRepository blogpostRepo;
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepo;

        public BlogController(IBlogRepository blogpostRepo, IMapper mapper, ICategoryRepository categoryRepo)
        {
            this.blogpostRepo = blogpostRepo;
            this.mapper = mapper;
            this.categoryRepo = categoryRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts(int page)
        {
            var blogs = await this.blogpostRepo.GetAllBlogs(page);
            if (blogs == null) return NotFound();
            //return Ok(mapper.Map<List<DTO.BlogPosts>>(blogs));
            var response = new List<DTO.BlogPosts>();
            foreach(var blog in blogs)
            {
                response.Add(new DTO.BlogPosts
                {
                    Id = blog.Id,
                    Author = blog.Author,
                    Content = blog.Content,
                    FeaturedImgURL = blog.FeaturedImgURL,
                    IsVisible = blog.IsVisible,
                    PublishedDate = blog.PublishedDate,
                    ShortDesc = blog.ShortDesc,
                    Title = blog.Title,
                    UrlHandle = blog.UrlHandle,
                    Categories = blog.Categories.Select(x => new DTO.Category
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] DTO.CreateBlog blog)
        {
            //var blogDet = mapper.Map<BlogPosts>(blog);
            var DomainBlog = new BlogPosts
            {
                Title = blog.Title,
                Author = blog.Author,
                Content = blog.Content,
                IsVisible = blog.IsVisible,
                UrlHandle = blog.UrlHandle,
                ShortDesc = blog.ShortDesc,
                PublishedDate = blog.PublishedDate,
                FeaturedImgURL = blog.FeaturedImgURL,
                Categories = new List<Category>()
            };
            if (DomainBlog.UrlHandle == null) DomainBlog.UrlHandle = DomainBlog.Title;
            DomainBlog.UrlHandle = await this.GenerateUniqueUrlHandle(DomainBlog.UrlHandle);
            foreach (var id in blog.CategoryIDs)
            {
                var blogDet = await this.categoryRepo.GetCategoryDetails(id);
                if (blogDet != null)
                {
                    DomainBlog.Categories.Add(blogDet);
                }
            }
            var createdBlog = await this.blogpostRepo.CreateBlog(DomainBlog);
            if (createdBlog == null) return NotFound();
            return Ok(mapper.Map<DTO.BlogPosts>(createdBlog));
        }
        [HttpGet]
        [Route("{blogId:guid}")]
        public async Task<IActionResult> GetPostDetails([FromRoute] Guid blogId)
        {
            var blogDet = await this.blogpostRepo.GetABlog(blogId);
            if(blogDet == null) return NotFound();
            return Ok(mapper.Map<DTO.BlogPosts>(blogDet));
        }
        [HttpPut]
        [Route("{blogId:guid}")]
        public async Task<IActionResult> UpdateBlog([FromRoute] Guid blogId, [FromBody] DTO.UpdateBlog blog)
        {
            var updatedDet = await this.blogpostRepo.UpdateBlog(blogId, blog);
            if (updatedDet == null) return NotFound();
            return Ok(mapper.Map<DTO.BlogPosts>(updatedDet));
        }
        [HttpDelete]
        [Route("{blogId:guid}")]
        public async Task<IActionResult> DeleteBlog([FromRoute] Guid blogId)
        {
            var updatedDet = await this.blogpostRepo.DeleteBlog(blogId);
            if (updatedDet == null) return NotFound();
            return Ok(mapper.Map<DTO.BlogPosts>(updatedDet));
        }
        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetBlogByUrlHandle([FromRoute] string urlHandle)
        {
            var blog = await blogpostRepo.GetBlogByUrl(urlHandle);
            if (blog == null) return NotFound();
            return Ok(mapper.Map<BlogPosts>(blog));
        }
        private async Task<string> GenerateUniqueUrlHandle(string urlHandle)
        {
            var newUrlHandle = urlHandle.ToLower().Replace(" ", "-");
            while(await this.blogpostRepo.GetBlogByUrl(newUrlHandle) != null)
            {
                newUrlHandle = $"{newUrlHandle}-{Guid.NewGuid().ToString().Substring(0, 4)}";
            }
            return newUrlHandle;
        }
    }
}
