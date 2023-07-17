
using System.Text.Json;

namespace MallMartLogic
{
    /// <summary>
    /// A class built to get "product" from FakeStoreApi, the way it's built there
    /// </summary>
    public class FakeStoreProduct
    {
        public int id { get; set; }
        public string title { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string image { get; set; }
        public Rating rating { get; set; }


        public List<FakeStoreProduct> GetProductsFromJson(string json)
        {
            var list = JsonSerializer.Deserialize<List<FakeStoreProduct>>(json);
            return list;
        }
    }
    /// <summary>
    /// A class built to get "Rating" from FakeStoreApi, the way it's built there
    /// </summary>
    public class Rating
    {
        public float rate { get; set; }
        public int count { get; set; }
    }
}
