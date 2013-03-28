
namespace CQRSDemo.Events
{
    public class CustomerAddressUpdatedEvent : DomainEvent
    {
        public string Address { get; set; }
        public string PostCode { get; set; }
    }
}
