using NServiceBus;
using System;
using CQRSDemo.Events;

namespace CQRSDemo.ReadModelAC
{
    class NewCustomerCreatedEventHandler : IHandleMessages<NewCustomerCreatedEvent>
    {
        public IBus Bus { get; set; }
        ReadContext context;

        public NewCustomerCreatedEventHandler()
        {
            context = new ReadContext();
        }

        public void Handle(NewCustomerCreatedEvent message)
        {
            //  Write the message to the console to show it has been received.
            Console.WriteLine("=============================================");
            Console.WriteLine("NewCustomerCreatedEvent received");
            Console.WriteLine("\tName:\t{0} {1}", message.FirstName, message.LastName);
            Console.WriteLine("\tAddress:\t{0}", message.Address);
            Console.WriteLine("\tPostCode:\t{0}", message.PostCode);

            var customer = new CustomerReadModel
            {
                CustomerId = message.CustomerId,
                FirstName = message.FirstName,
                LastName = message.LastName,
                Address = message.Address,
                PostCode = message.PostCode
            };

            context.Customers.Add(customer);
            context.SaveChanges();
        }
    }
}
