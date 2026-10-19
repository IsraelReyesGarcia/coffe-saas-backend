using System;
using AutoMapper;
using Cafeteria.Models.Dtos.Order;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.OrderProduct;

namespace Cafeteria.Mapping;

public class OrderProfile:Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.OrderProducts));
        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.Table, opt => opt.Ignore())
            .ForMember(dest => dest.OrderProducts, opt => opt.Ignore());

        CreateMap<CreateOrderDto, Order>()
            .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.Details))
            .ForMember(dest => dest.Table, opt => opt.Ignore())
            .ForMember(dest => dest.Client, opt => opt.Ignore());

        CreateMap<Order, CancelOrderDto>().ReverseMap();
        CreateMap<Order, PayOrderDto>().ReverseMap();

        // Mapeo del detalle de la orden
        CreateMap<CreateOrderProductDto, OrderProduct>()
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.OrderProductId, opt => opt.Ignore());
        CreateMap<OrderProduct, CreateOrderProductDto>();
    }
}
