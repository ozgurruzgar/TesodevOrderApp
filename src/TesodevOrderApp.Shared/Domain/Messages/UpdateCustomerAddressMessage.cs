using TesodevOrderApp.Shared.Domain.Models;

namespace TesodevOrderApp.Shared.Domain.Messages
{
    public class UpdateCustomerAddressMessage
    {
        public Guid CustomerId { get; set; }
        public Address Address { get; set; }
    }
}
