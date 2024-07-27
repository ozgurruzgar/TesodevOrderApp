using TesodevOrderApp.Shared.Domain.Models.Base;

namespace Domain.Models
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public Product() { }
    }
}
