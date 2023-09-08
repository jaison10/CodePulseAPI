using AutoMapper;
using DomainModels =  CodePulseAPI.Models.DomainModels;
using CodePulseAPI.Models.DTO;

namespace CodePulseAPI.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            CreateMap<Category, DomainModels.Category>();
        }
    }
}
