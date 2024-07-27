namespace WebAPI.Models.Request
{
    public class CreateCustomerRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressRequestModel Address { get; set; }
    }
}
