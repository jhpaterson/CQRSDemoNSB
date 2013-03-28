using System;

namespace CQRSDemo.Events
{
    public class NewCustomerCreatedEvent : DomainEvent 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public bool IsApproved { get; set; }
    }
}
