﻿using CodePulseAPI.Data;
using CodePulseAPI.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace CodePulseAPI.Repositories
{
    public class CategoryRepoImplement : ICategoryRepository
    {
        private readonly CodePulseDbContext context;

        public CategoryRepoImplement(CodePulseDbContext dbContext) {
            this.context = dbContext;
        }
        public async Task<Category> CreateNewCategory(Category NewCategoryDet)
        {
            var student = await context.Categories.AddAsync(NewCategoryDet);
            await context.SaveChangesAsync();
            return student.Entity;
        }
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await context.Categories.ToListAsync();
        }
    }
}