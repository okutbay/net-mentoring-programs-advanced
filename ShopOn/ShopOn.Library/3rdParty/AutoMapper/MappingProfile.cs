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
        #region Carting
        CreateMap<CartItem, CartItemDto>()
            .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
            .ReverseMap();
        #endregion

        #region Cataloging
        CreateMap<CatalogItem, CatalogItemDto>()
            .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
            .ReverseMap();
        #endregion
    }
}
