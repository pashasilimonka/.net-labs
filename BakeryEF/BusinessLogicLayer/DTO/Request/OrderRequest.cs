namespace BusinessLogicLayer.DTO.Request
{
    public class OrderRequest
    {
        public int order_id { get; set; }
        public float summary_price { get; set; }
        public DateTime orderedAt { get; set; }
        public DateTime? receivedAt { get; set; }
        public int client_id { get; set; }
    }
}
