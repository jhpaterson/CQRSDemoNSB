using System;
using System.ComponentModel.DataAnnotations;

namespace CQRSDemo.ReadModelAC
{
    public class CustomerReadModel
    {
        [Key]
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
    }
}
