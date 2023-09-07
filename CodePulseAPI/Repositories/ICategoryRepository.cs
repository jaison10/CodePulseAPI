using CodePulseAPI.Models.DTO;

namespace CodePulseAPI.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> CreateNewCategory(Category NewCategoryDet);
    }
}
