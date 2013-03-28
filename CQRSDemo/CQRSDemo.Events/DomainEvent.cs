using NServiceBus;
using System;

namespace CQRSDemo.Events
{
    public class DomainEvent : IMessage
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
