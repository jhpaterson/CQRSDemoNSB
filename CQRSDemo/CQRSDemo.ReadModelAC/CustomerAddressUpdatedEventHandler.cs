using CQRSDemo.Events;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSDemo.ReadModelAC
{
    public class CustomerAddressUpdatedEventHandler : IHandleMessages<CustomerAddressUpdatedEvent>
    {
        public IBus Bus { get; set; }
        ReadContext context;

        public CustomerAddressUpdatedEventHandler()
        {
            context = new ReadContext();
        }

        public void Handle(CustomerAddressUpdatedEvent message)
        {
            //  Write the message to the console to show it has been received.
            Console.WriteLine("=============================================");
            Console.WriteLine("CustomerAddressUpdatedEvent received");
            Console.WriteLine("\tCustomerId:\t{0}", message.CustomerId);
            Console.WriteLine("\tAddress:\t{0}", message.Address);
            Console.WriteLine("\tPostCode:\t{0}", message.PostCode);

            var customer = context.Customers.
                Where(c => c.CustomerId == message.CustomerId).
                FirstOrDefault();

            customer.Address = message.Address;
            customer.PostCode = message.PostCode;
            context.SaveChanges();
        }
    }
}
