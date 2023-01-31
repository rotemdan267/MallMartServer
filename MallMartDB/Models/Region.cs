using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMartDB.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<EmployeeRegion>? Employees { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
