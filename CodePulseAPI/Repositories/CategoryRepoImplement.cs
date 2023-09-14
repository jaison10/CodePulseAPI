using CodePulseAPI.Data;
using CodePulseAPI.Models.DomainModels;
using DTO = CodePulseAPI.Models.DTO;
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
        public async Task<Category?> GetCategoryDetails(Guid catId)
        {
            return await context.Categories.FindAsync(catId);
        }
        public async Task<Category?> UpdateCategory(Guid catId, DTO.UpdateCategory category)
        {
            var existingCategory = await GetCategoryDetails(catId);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.UrlHandle = category.UrlHandle;

                await context.SaveChangesAsync();
                return existingCategory;
            }
            else
            {
                return null;
            }
        }
    }
}
