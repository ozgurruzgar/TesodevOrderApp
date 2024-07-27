using TesodevOrderApp.Shared.Domain.Models;

namespace WebAPI.Models.Response
{
    public class GetCustomerByIdResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
