using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO.Responce
{
    public class OrderProductResponce
    {
        public int order_id { get; set; }
        public float summary_price { get; set; }
        public DateTime orderedAt { get; set; }
        public DateTime? receivedAt { get; set; }
        public ICollection<ProductResponce> products { get; set; }
    }
}
