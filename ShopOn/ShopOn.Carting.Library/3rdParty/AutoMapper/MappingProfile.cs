using AutoMapper;
using ShopOn.Carting.Library.Entities;
using ShopOn.Carting.Library.Models;

namespace ShopOn.Carting.Library._3rdParty.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Carting
        CreateMap<CartItem, CartItemDto>()
            .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
            .ReverseMap();
    }
}
