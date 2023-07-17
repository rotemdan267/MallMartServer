
namespace MallMartDB.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        //איש הקשר אצל הספק
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
