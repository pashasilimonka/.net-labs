namespace DataAccessLayer.Parameters
{
    public class OrderParameters:QueryStringParameters
    {
        public float? summary_price { get; set; }
        public DateTime? orderedAt { get; set; }
        public DateTime? receivedAt { get; set; }
        public int? client_id { get; set; }
    }
}
