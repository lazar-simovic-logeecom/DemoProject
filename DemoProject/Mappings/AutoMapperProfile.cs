using AutoMapper;
using DemoProject.Dto;
using DemoProject.Data.Model;

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