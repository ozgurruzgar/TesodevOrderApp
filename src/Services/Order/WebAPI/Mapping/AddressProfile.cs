using AutoMapper;
using TesodevOrderApp.Shared.Domain.Models;
using WebAPI.Models.Request;

namespace WebAPI.Mapping
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<AddressRequestModel, Address>();
        }
    }
}
