using CQRSDemo.Commands;
using NServiceBus;
using System;
using System.Linq;
using CQRSDemo.Events;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.Linq;
using Db4objects.Db4o.Query;

namespace CQRSDemo.DomainAC
{
    public class UpdateCustomerAddressCommandHandler : IHandleMessages<UpdateCustomerAddressCommand>
    {
        public IBus Bus { get; set; }
        Db4oObjectContainerFactory db4o;

        public UpdateCustomerAddressCommandHandler()
        {
            db4o = new Db4oObjectContainerFactory("eventstore.db4o");
        }

        public void Handle(UpdateCustomerAddressCommand message)
        {
            //  Write the message to the console to show it has been received.
            Console.WriteLine("=============================================");
            Console.WriteLine("UpdateCustomerAddessCommand received");
            Console.WriteLine("\tCustomerId:\t{0}", message.CustomerId);
            Console.WriteLine("\tAddress:\t{0}", message.Address);
            Console.WriteLine("\tPostCode:\t{0}", message.PostCode);

            // reconstitute current state of customer from events
            // this example simply gets the most recent create and update
            // events and uses their properties to set the customer properties
            // in some situations the current entity state could depend on the
            // accumulated effect of all events, e.g. balance after transactions

            //NewCustomerCreatedEvent createEvent;
            //CustomerAddressUpdatedEvent updateEvent;

            using (IObjectContainer client = db4o.Create())
            {
                IQueryable<DomainEvent> query = client.AsQueryable<DomainEvent>();

                var createEvent = query.
                    OfType<NewCustomerCreatedEvent>().
                    Where(e => e.CustomerId == message.CustomerId).
                    OrderByDescending(e => e.TimeStamp).
                    FirstOrDefault();

                var updateEvent = query.
                    OfType<CustomerAddressUpdatedEvent>().
                    Where(e => e.CustomerId == message.CustomerId).
                    OrderByDescending(e => e.TimeStamp).
                    FirstOrDefault();

                if (updateEvent == null)
                {
                    updateEvent = new CustomerAddressUpdatedEvent
                    {
                        Address = createEvent.Address,
                        PostCode = createEvent.PostCode
                    };
                }

                var customerAggregate = new Customer(createEvent.CustomerId, createEvent.FirstName,
                    createEvent.LastName, updateEvent.Address, updateEvent.PostCode);

                // update address from command -  - some business logic might happen here
                var address = new Address
                {
                    Street = message.Address,
                    PostCode = message.PostCode
                };

                customerAggregate.Address = address;

                // create and publish new updated event
                CustomerAddressUpdatedEvent cadEvent = new CustomerAddressUpdatedEvent
                {
                    CustomerId = customerAggregate.CustomerId,
                    Address = customerAggregate.Address.Street,
                    PostCode = customerAggregate.Address.PostCode,
                    TimeStamp = DateTime.Now
                };

                client.Store(cadEvent);
                
                Bus.Send(cadEvent);
            }

            
        }


    }
}
