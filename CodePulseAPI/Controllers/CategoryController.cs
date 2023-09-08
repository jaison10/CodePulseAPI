﻿using CodePulseAPI.Models.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodePulseAPI.Repositories;
using DTO = CodePulseAPI.Models.DTO;

namespace CodePulseAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
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
            return Ok(returnCategory);
        }
    }
}
