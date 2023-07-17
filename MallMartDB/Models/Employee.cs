

namespace MallMartDB.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? ManagerId { get; set; }
        public Job JobTitle { get; set; }
        public List<EmployeeRegion> DeliveryRegions { get; set; }
        public Employee Manager { get; set; }
        public List<Employee> Employees { get; set; }

        public override string ToString()
        {
            return $"{User.FirstName} {User.LastName} - {JobTitle.ToString()}";
        }
    }
}
