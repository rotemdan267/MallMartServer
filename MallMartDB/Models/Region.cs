
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
