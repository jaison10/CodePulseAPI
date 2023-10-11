using AutoMapper;
using CodePulseAPI.Models.DomainModels;
using DTO = CodePulseAPI.Models.DTO;

namespace CodePulseAPI.Profiles.AfterMaps
{
    public class BlogCategoryAfterMap : IMappingAction<List<BlogPosts>, List<DTO.BlogPosts>>
    {
        public async void Process(List<BlogPosts> source, List<DTO.BlogPosts> destination, ResolutionContext context)
        {

        }
    }
}