using Domain.Models;

namespace WebAPI.Models.Response
{
    public class GetOrderByCustomerIdResponseModel
    {
        public List<Order> Orders { get; set; }
    }
}
