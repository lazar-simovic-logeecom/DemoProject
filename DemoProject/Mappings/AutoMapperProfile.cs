using AutoMapper;
using DemoProject.Dto;
using DemoProject.Domain;

namespace DemoProject.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SubCategories, opt => opt.Ignore());
    }
}