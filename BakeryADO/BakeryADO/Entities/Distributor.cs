namespace BakeryADO.Entities
{
    public class Distributor
    {
        public int distributor_id { get; set; }
        public string country { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone_number { get; set; }
        public List<Ingredient> ingredients { get; set;}
    }
}
