
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
