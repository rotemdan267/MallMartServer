
namespace MallMartDB.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime? DateOrdered { get; set; }

        // We'll let our customer choose when to get his delivery.
        // The choice will be a range of time between DueTimeFirst and DueTimeLast
        public DateTime? DueTimeFirst { get; set; }
        public DateTime? DueTimeLast { get; set; }

        // When did the delivery actually arrived
        public DateTime? ArrivalTime { get; set; }

        // The employee assinged to deliver this order
        public int? EmployeeId { get; set; }

        public float? TotalPrice { get; set; }
        public float? PricePaid { get; set; }
        public bool IsPaid
        {
            get
            {
                if (PricePaid == null || TotalPrice == null)
                    return false;
                if (PricePaid < TotalPrice)
                {
                    return false;
                }
                else return true;
            }
        }
        public bool IsOrderDone { get; set; }
        public string? Comment { get; set; }

        public Employee? Employee { get; set; }
        public List<OrderLine> OrderLines { get; set; }

        public void SetTotalPrice()
        {
            TotalPrice = 0;
            foreach (var item in OrderLines)
            {
                TotalPrice += item.UnitPrice * item.Quantity;
            }
        }
        public override string ToString()
        {
            return $"{DateOrdered} by {Customer.User.FirstName} + {Customer.User.LastName} - {TotalPrice}";
        }        
    }
}
