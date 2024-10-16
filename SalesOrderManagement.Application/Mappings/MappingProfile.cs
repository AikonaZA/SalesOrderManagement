using AutoMapper;
using SalesOrderManagement.Application.DTOs.Order;
using SalesOrderManagement.Application.DTOs.SalesOrder;
using SalesOrderManagement.Core.Models.Domain;

namespace SalesOrderManagement.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        SalesOrder();
        Order();
    }

    private void Order()
    {
        // Map OrderDto to Order domain model and reverse map
        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.OrderRef, opt => opt.MapFrom(src => src.OrderRef))
            .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines))
            .ReverseMap();

        // Map OrderLineDto to OrderLine domain model and reverse map
        CreateMap<OrderLineDto, OrderLine>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
            .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.Quantity))
            .ReverseMap();
    }

    private void SalesOrder()
    {
        // Map SalesOrderDto to Order domain model and reverse map
        CreateMap<SalesOrderDto, Order>()
            .ForMember(dest => dest.OrderRef, opt => opt.MapFrom(src => src.SalesOrderRef))
            .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines))
            .ReverseMap();

        // Map SalesOrderLineDto to OrderLine domain model and reverse map
        CreateMap<SalesOrderLineDto, OrderLine>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.SkuCode))
            .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.Quantity))
            .ReverseMap();
    }
}