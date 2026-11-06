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
        CreateMap<CatalogItem, CatalogItemDto>()
            .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
            .ReverseMap();

        CreateMap<Category, CategoryDto>()
            .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
            .ReverseMap();

        CreateMap<Product, ProductDto>()
            .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
            .ReverseMap();
    }
}
