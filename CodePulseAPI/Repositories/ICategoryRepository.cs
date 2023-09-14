using CodePulseAPI.Models.DomainModels;
using DTO = CodePulseAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CodePulseAPI.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> CreateNewCategory(Category NewCategoryDet);
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category?> GetCategoryDetails(Guid catId);
        Task<Category?> UpdateCategory(Guid catId, DTO.UpdateCategory category);
    }
}
