using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMartDB.Models
{
    public class AcquisitionOrder
    {
        public int AcquisitionOrderId { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime? DateOrdered { get; set; }

        // נאפשר ללקוח לבחור מתי המשלוח יגיע אליו, בטווח בין הזמן הראשון לאחרון
        public DateTime? DueTimeFirst { get; set; }
        public DateTime? DueTimeLast { get; set; }

        // מתי המשלוח הגיע בפועל
        public DateTime? ArrivalTime { get; set; }

        // העובד שקיבל את ההזמנה
        public int? EmployeeId { get; set; }
        public float? TotalPrice { get; set; }
        public float? PricePaid { get; set; }
        public bool IsOrderDone { get; set; }
        public string Comment { get; set; }

        public Employee Employee { get; set; }
        public List<AcquisitionOrderLine> OrderLines { get; set; }

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
            return $"{DateOrdered} from {Supplier.Name} - {TotalPrice}";
        }
    }
}
