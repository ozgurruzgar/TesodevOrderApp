﻿using TesodevOrderApp.Shared.Domain.Models;

namespace Domain.Events
{
    public class SendAdressMessage
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public Address Address { get; set; }
        public string Product { get; set; }
    }
}
