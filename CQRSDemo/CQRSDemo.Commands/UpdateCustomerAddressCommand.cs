using System;

namespace CQRSDemo.Commands
{
    public class UpdateCustomerAddressCommand : DomainCommand
    {
        public Guid CustomerId { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
    }
}
