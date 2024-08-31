using AutoMapper;
using SalesOrderManagement.Application.DTOs.SalesOrder;
using SalesOrderManagement.Core.Models.Domain;
using SalesOrderLine = SalesOrderManagement.Application.DTOs.SalesOrder.SalesOrderLineDto;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Map SalesOrderRequestDto to Order domain model
        CreateMap<SalesOrderDto, Order>()
            .ForMember(dest => dest.OrderRef, opt => opt.MapFrom(src => src.SalesOrderRef))
            .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines));

        CreateMap<SalesOrderLine, OrderLine>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.SkuCode))
            .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.Quantity));
    }
}