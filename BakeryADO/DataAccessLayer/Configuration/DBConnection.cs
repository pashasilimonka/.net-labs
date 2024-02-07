using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Configuration
{
    public class DBConnection
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DBConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public SqlConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
