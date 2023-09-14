using AutoMapper;
using CodePulseAPI.Models.DomainModels;
using DTO = CodePulseAPI.Models.DTO;

namespace CodePulseAPI.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            CreateMap<Category, DTO.Category>();
            CreateMap<BlogPosts, DTO.BlogPosts>();
            CreateMap<DTO.CreateBlog, BlogPosts> ();
        }
    }
}
