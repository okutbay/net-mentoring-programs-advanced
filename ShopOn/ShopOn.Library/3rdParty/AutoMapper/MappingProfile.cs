using AutoMapper;
using ShopOn.Library.Entities.Carting;
using ShopOn.Library.Entities.Cataloging;
using ShopOn.Library.Models.Carting;
using ShopOn.Library.Models.Cataloging;

namespace ShopOn.Library._3rdParty.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Carting
        CreateMap<CartItem, CartItemDto>()
            .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
            .ReverseMap();

        // Cataloging
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategory != null ? src.ParentCategory.ExtGuid : null))
            .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.ParentCategory, opt => opt.Ignore())
            .ForMember(dest => dest.Children, opt => opt.Ignore());

        CreateMap<Product, ProductDto>()
            .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
            .ReverseMap();
    }
}
