using System;
using AutoMapper;
using Cafeteria.Models.Dtos.Order;
using Cafeteria.Models;

namespace Cafeteria.Mapping;

public class OrderProfile:Profile
{
    public OrderProfile()
    {
        CreateMap<Order,OrderDto>().ReverseMap();
        CreateMap<Order,CreateOrderDto>().ReverseMap();
        CreateMap<Order,CancelOrderDto>().ReverseMap();
        CreateMap<Order,PayOrderDto>().ReverseMap();
    }
}
