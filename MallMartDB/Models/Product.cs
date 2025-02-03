
namespace MallMartDB.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public float? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public int? UnitsOnOrder { get; set; } // How many units were ordered and weren't delievered yet
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public float? Rating { get; set; }
        public int? NumOfRaters { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }

    }
}
