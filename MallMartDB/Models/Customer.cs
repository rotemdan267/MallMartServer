

namespace MallMartDB.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string PaymentDetails { get; set; }
        public List<Order> Orders { get; set; }

        public override string ToString()
        {
            return $"{User.FirstName} {User.LastName}";
        }
    }
}
