using TesodevOrderApp.Shared.Domain.Models;

namespace Domain.Args
{
    public class UpdateOrderArgs
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Address Address { get; set; }
    }
}
