

using DataAccessLayer.Entities;

namespace BusinesLogicLayer.DTOs
{
    public class ProductIngredients
    {
        public int product_id { get; set; }
        public string? dame { get; set; }
        public string? description { get; set; }
        public string? recipe { get; set; }
        public double? price { get; set; }
        public List<Ingredient> ingredient_id { get; set;}
    }
}
