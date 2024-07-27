namespace TesodevOrderApp.Shared.Domain.Constants
{
    public class Constants
    {
        public struct Queues
        {
            public Queues() { }

            public const string FillAddressQueueName = "queue:create-order-fill-address";
            public const string SendAddressQueueName = "queue:customer-create-order-address";
            public const string UpdateCustomerAddressQueueName = "queue:order-customer-update-address";
            public const string FillAddressQueue = "create-order-fill-address";
            public const string SendAddressQueue = "customer-create-order-address";
            public const string UpdateCustomerAddressQueue = "order-customer-update-address";
        }

        public struct ExceptionMessages
        {
            public ExceptionMessages() { }

            public const string CustomerNotFound = "Customer was not found.";
            public const string InvalidOrder = "Order not valid.";
        }
    }
}
