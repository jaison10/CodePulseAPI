using CodePulseAPI.Models.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodePulseAPI.Repositories;
using DTO = CodePulseAPI.Models.DTO;
using AutoMapper;

namespace CodePulseAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] DTO.CreateCategory CategoryDetails)
        {
            var categoryDet = new Category
            {
                //Id = new Guid(),
                Name = CategoryDetails.Name,
                UrlHandle = CategoryDetails.UrlHandle,
            };
            var returnCategory = await this.categoryRepository.CreateNewCategory(categoryDet);
            return Ok(mapper.Map<DTO.Category> (returnCategory));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var allCategories = await this.categoryRepository.GetAllCategories();
            var afterMapping = mapper.Map<List<DTO.Category>>(allCategories);
            return Ok(afterMapping);
        }
        [HttpGet]
        [Route("{catId:guid}")]
        public async Task<IActionResult> GetCategoryDetails([FromRoute] Guid catId)
        {
            var categoryDet = await this.categoryRepository.GetCategoryDetails(catId);
            if (categoryDet != null)
            {
                return Ok(mapper.Map<DTO.Category>(categoryDet));
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("{catId:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid catId, [FromBody] DTO.UpdateCategory category)
        {
            var categoryDet = await this.categoryRepository.UpdateCategory(catId, category);
            if (categoryDet != null)
            {
                return Ok(mapper.Map<DTO.Category>(categoryDet));
            }
            else
            {
                return NotFound();
            }
        }

    }
}
