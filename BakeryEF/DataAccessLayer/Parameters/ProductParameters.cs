namespace DataAccessLayer.Parameters
{
    public class ProductParameters:QueryStringParameters
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public float? price { get; set; }
    }
}
