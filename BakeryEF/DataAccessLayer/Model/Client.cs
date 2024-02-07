using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Model
{
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int client_id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string phone_number { get; set; }

        public ICollection<Order>  orders { get; set; }
    }
}
