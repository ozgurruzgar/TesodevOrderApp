namespace WebAPI.Models.Request
{
    public class CreateOrderRequestModel
    {
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public ProductRequestModel Product { get; set; }
    }
}
