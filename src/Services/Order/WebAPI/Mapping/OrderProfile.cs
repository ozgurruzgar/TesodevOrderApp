using Application.Commands;
using AutoMapper;
using Domain.Args;
using Domain.Models;
using System.Text.Json;
using WebAPI.Models.Request;
using WebAPI.Models.Response;

namespace WebAPI.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderRequestModel, CreateOrderCommand>()
                .AfterMap<ProductAction>();

            CreateMap<CreateOrderCommand, FillAddressArgs>();

            CreateMap<UpdateOrderRequestModel, UpdateOrderCommand>();
            CreateMap<DeleteOrderRequestModel, DeleteOrderCommand>();
            CreateMap<ChangeStatusRequestModel, ChangeStatusCommand>();

            CreateMap<Order, GetOrderByIdResponseModel>();
            CreateMap<List<Order>, GetOrderByCustomerIdResponseModel>()
                .ForMember(c => c.Orders, c => c.MapFrom(c => c));
        }
    }

    public class ProductAction : IMappingAction<CreateOrderRequestModel, CreateOrderCommand>
    {
        public void Process(CreateOrderRequestModel source, CreateOrderCommand destination, ResolutionContext context)
        {
            destination.Product = JsonSerializer.Serialize(source.Product);
        }
    }
}
