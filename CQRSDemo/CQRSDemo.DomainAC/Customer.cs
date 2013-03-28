using System;

namespace CQRSDemo.DomainAC
{
    public class Customer
    {

        private Guid customerId;

        public Guid CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private Address address;

        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        private bool isApproved;

        public bool IsApproved
        {
            get { return isApproved; }
            set { isApproved = value; }
        }

        public Customer(Guid customerId, string firstName, string lastName, string address, string postCode)
        {
            this.customerId = customerId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = new Address { Street = address, PostCode = postCode };
            // could be some  validation/initialisation business logic here - could be a "saga"
            // simple example - new customer has to be approved so status is initially unapproved
            this.isApproved = false;
        }

    }
}
