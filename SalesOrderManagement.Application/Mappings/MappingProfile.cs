using AutoMapper;
using SalesOrderManagement.Application.DTOs;
using SalesOrderManagement.Core.Models.Domain;

namespace SalesOrderManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderLine, OrderLineDto>().ReverseMap();
        }
    }
}