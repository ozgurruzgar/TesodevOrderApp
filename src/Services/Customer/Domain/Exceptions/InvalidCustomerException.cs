namespace Domain.Exceptions
{
    public class InvalidCustomerException : Exception
    {
        public InvalidCustomerException(string message) : base(message) { }
    }

    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(string message) : base(message) { }
    }
}
