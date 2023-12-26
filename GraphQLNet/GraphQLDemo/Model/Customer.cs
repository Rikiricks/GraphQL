namespace GraphQLDemo.Model
{
    public class Customer
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class CustomerInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class ResponseCustomerCollectionType
    {
        public List<Customer> Customers { get; set; }
    }

    public class ResponseCustomerType
    {
        public Customer Customer { get; set; }
        public string message { get; set; }
    }
}
