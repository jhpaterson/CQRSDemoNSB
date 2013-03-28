using System;

namespace CQRSDemo.Commands
{
    public class CreateNewCustomerCommand : DomainCommand
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
    }
}
