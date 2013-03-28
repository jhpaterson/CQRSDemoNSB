using System;
using NServiceBus;
using CQRSDemo.Commands;
using CQRSDemo.Events;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;

namespace CQRSDemo.DomainAC
{
    public class CreateNewCustomerCommandHandler : IHandleMessages<CreateNewCustomerCommand>
    {
        public IBus Bus { get; set; }
        Db4oObjectContainerFactory db4o;

        public CreateNewCustomerCommandHandler()
        {
            db4o = new Db4oObjectContainerFactory("eventstore.db4o");
        }

        public void Handle(CreateNewCustomerCommand message)
        {
            //  Write the message to the console to show it has been received.
            Console.WriteLine("=============================================");
            Console.WriteLine("CreateNewCustomerCommand received");
            Console.WriteLine("\tName:\t{0} {1}", message.FirstName, message.LastName);
            Console.WriteLine("\tAddress:\t{0}", message.Address);
            Console.WriteLine("\tPostCode:\t{0}", message.PostCode);

            var customerAggregate = new Customer(message.CustomerId, message.FirstName,
                    message.LastName, message.Address, message.PostCode);

            // create and publish new created event
            NewCustomerCreatedEvent nccEvent = new NewCustomerCreatedEvent
            {
                CustomerId = customerAggregate.CustomerId,
                FirstName = customerAggregate.FirstName,
                LastName = customerAggregate.LastName,
                Address = customerAggregate.Address.Street,
                PostCode = customerAggregate.Address.PostCode,
                TimeStamp = DateTime.Now,
                IsApproved = customerAggregate.IsApproved
            };

            using (IObjectContainer client = db4o.Create())
            {
                client.Store(nccEvent);
            }

            Bus.Send(nccEvent);
        }
    }
}
