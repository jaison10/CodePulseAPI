using CodePulseAPI.Models.DomainModels;

namespace CodePulseAPI.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> CreateNewCategory(Category NewCategoryDet);
        Task<IEnumerable<Category>> GetAllCategories();
    }
}
