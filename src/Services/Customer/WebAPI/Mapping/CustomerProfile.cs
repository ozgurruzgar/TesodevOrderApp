using Application.Commands;
using AutoMapper;
using Domain.Models;
using WebAPI.Models.Request;
using WebAPI.Models.Response;

namespace WebAPI.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerRequestModel, CreateCustomerCommand>();
            CreateMap<UpdateCustomerRequestModel, UpdateCustomerCommand>();
            CreateMap<DeleteCustomerRequestModel, DeleteCustomerCommand>();

            CreateMap<Customer, GetCustomerByIdResponseModel>();
        }
    }
}
