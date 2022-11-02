using Order = Core.Entites.OrderAggregate.Order;
using Product = Core.Entites.Product;
namespace API.Helpers;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDto>()
        .ForMember(p => p.ProductType, o => o.MapFrom(s => s.ProductType.Name))
        .ForMember(p => p.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
        .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

        CreateMap<Core.Entites.Identity.Address, AddressDto>().ReverseMap();

        CreateMap<CustomerBaskerDto, CustomerBasket>();
        CreateMap<BasketItemDto, BasketItem>();

        CreateMap<AddressDto, Core.Entites.OrderAggregate.Address>();

        CreateMap<Order, OrderToReturnDto>()
            .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());

    }
}
