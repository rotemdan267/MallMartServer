
namespace MallMartDB.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime? DateOrdered { get; set; }

        // נאפשר ללקוח לבחור מתי המשלוח יגיע אליו, בטווח בין הזמן הראשון לאחרון
        public DateTime? DueTimeFirst { get; set; }
        public DateTime? DueTimeLast { get; set; }

        // מתי המשלוח הגיע בפועל
        public DateTime? ArrivalTime { get; set; }

        // השליח שיספק את ההזמנה
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
