using DataAccessLayer.Model;
using MassTransit;

namespace BusinessLogicLayer.DTO.Request
{
    public class ProductRequest
    {
        public int product_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float? price { get; set; }

    }
}
