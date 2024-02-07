namespace DataAccessLayer.Entities
{
    public class Ingredient
    {
        public int ingredient_id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        
        public int? distributor_id { get; set; }

    }
}
