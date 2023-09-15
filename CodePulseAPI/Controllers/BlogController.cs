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

        public BlogController(IBlogRepository blogpostRepo, IMapper mapper)
        {
            this.blogpostRepo = blogpostRepo;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var blogs = await this.blogpostRepo.GetAllBlogs();
            if (blogs == null) return NotFound();
            return Ok(mapper.Map<List<DTO.BlogPosts>>(blogs));
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] DTO.CreateBlog blog)
        {
            var blogDet = mapper.Map<BlogPosts>(blog);
            var createdBlog = await this.blogpostRepo.CreateBlog(blogDet);
            if(createdBlog == null) return NotFound();
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
    }
}
