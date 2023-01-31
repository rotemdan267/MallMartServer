using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMartDB.Models
{
    public class EmployeeRegion
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int RegionId { get; set; }

        public Employee Employee { get; set; }
        public Region Region { get; set; }
    }
}
