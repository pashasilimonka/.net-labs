namespace DataAccessLayer.Model
{
    public class Product
    {
        public int product_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public ICollection<Order> orders { get; set; }
    }
}
