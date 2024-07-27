using AutoMapper;
using Domain.Models;
using WebAPI.Models.Request;

namespace WebAPI.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequestModel, Product>();
        }
    }
}
