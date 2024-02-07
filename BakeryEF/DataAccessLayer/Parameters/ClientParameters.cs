
namespace DataAccessLayer.Parameters
{
    public class ClientParameters:QueryStringParameters
    {
        public string? name { get; set; }
        public string? surname { get; set; }
        public string? phone_number { get; set; }

    }
}
