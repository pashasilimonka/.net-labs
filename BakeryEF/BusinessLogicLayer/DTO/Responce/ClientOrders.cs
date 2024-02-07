using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO.Responce
{
    public class ClientOrders
    {
        public int client_id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string phone_number { get; set; }
        public IEnumerable<OrderResponce> orders { get; set; }
    }
}
