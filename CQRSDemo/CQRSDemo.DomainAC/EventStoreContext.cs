
using CQRSDemo.Events;
using System.Data.Entity;

namespace CQRSDemo.DomainAC
{
    public class EventStoreContext : DbContext
    {
        public DbSet<DomainEvent> Events { get; set; }
    }
}
