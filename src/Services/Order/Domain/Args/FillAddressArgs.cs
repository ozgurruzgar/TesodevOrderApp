namespace Domain.Args
{
    public class FillAddressArgs
    {
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public string Product { get; set; }
    }
}
