using TesodevOrderApp.Shared.Domain.Models;
using TesodevOrderApp.Shared.Domain.Models.Base;

namespace Domain.Models
{
    public class Customer : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Customer() { }
    }
}
