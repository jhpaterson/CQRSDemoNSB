using System.Data.Entity;

namespace CQRSDemo.ReadModelAC
{
    public class ReadContext : DbContext
    {
        public DbSet<CustomerReadModel> Customers { get; set; }
    }
}
