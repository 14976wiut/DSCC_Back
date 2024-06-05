using AutoMapper;
using DSCC_Back.DTO;
using DSCC_Back.Models;

namespace DSCC_Back.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderReadDto>();
            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderUpdateDto, Order>();
        }
    }
}
