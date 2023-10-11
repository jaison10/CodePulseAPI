using AutoMapper;
using CodePulseAPI.Models.DomainModels;
using CodePulseAPI.Profiles.AfterMaps;
using DTO = CodePulseAPI.Models.DTO;

namespace CodePulseAPI.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            CreateMap<Category, DTO.Category>();
            CreateMap<BlogPosts, DTO.BlogPosts>();
            //CreateMap<List<BlogPosts>, List<DTO.BlogPosts>>().AfterMap<BlogCategoryAfterMap>();
            //CreateMap<DTO.CreateBlog, BlogPosts>().AfterMap<CategoriesForBlogsAfterMaps>();
        }
    }
}
