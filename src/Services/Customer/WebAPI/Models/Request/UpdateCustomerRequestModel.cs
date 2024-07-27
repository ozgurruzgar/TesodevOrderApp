namespace WebAPI.Models.Request
{
    public class UpdateCustomerRequestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressRequestModel Address { get; set; }
    }
}
