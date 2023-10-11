using AutoMapper;
using CodePulseAPI.Models.DomainModels;
using CodePulseAPI.Repositories;
using DTO = CodePulseAPI.Models.DTO;

namespace CodePulseAPI.Profiles.AfterMaps
{
    public class CategoriesForBlogsAfterMaps : IMappingAction<DTO.CreateBlog, BlogPosts>
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesForBlogsAfterMaps(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async void Process(DTO.CreateBlog source, BlogPosts destination, ResolutionContext context)
        {
            //destination.Categories = new List<Category>();
            //foreach(var eachGuid in source.CategoryIDs)
            //{
            //    var existingCategory = await categoryRepository.GetCategoryDetails(eachGuid);
            //    if (existingCategory != null)
            //    {
            //        destination.Categories.Add(existingCategory);
            //    }
            //}
        }
    }
}
