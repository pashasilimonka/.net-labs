namespace BusinessLogicLayer.DTO.Responce
{
    public class OrderResponce
    {
        public int order_id { get; set; }
        public float summary_price { get; set; }
        public DateTime orderedAt { get; set; }
        public DateTime? receivedAt { get; set; }
    }
}
