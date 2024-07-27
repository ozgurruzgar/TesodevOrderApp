namespace WebAPI.Models.Request
{
    public class UpdateOrderRequestModel
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public AddressRequestModel Address { get; set; }
    }
}
