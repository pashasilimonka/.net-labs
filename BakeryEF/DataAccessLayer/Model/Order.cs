using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Model
{
    public class Order
    {
        public int order_id {  get; set; }
        public float summary_price { get; set; }
        public DateTime orderedAt { get; set; }
        public DateTime? receivedAt { get; set; }
        public int client_id { get; set; }
        [ForeignKey("client_id")]
        public Client client { get; set; }
        public ICollection<Product> products { get; set; }
        
    }
}
