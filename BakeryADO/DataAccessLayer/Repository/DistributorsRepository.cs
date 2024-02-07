using Dapper;
using DataAccessLayer.Configuration;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces.Repository;
using Microsoft.Data.SqlClient;
using System.Data;



namespace DataAccessLayer.Repository
{
    public class DistributorsRepository : IDistributorsRepository
    {
        private readonly DBConnection dBConnection;
        public DistributorsRepository(DBConnection dBConnection)
        {
            this.dBConnection = dBConnection;
        }
        public async Task<int> createDistributor(Distributor distributor)
        {
           
            using (SqlConnection connection = dBConnection.CreateConnection())
            {
                var result = await connection.ExecuteAsync(
                    "INSERT INTO distributors (country,name,address,phone_number) VALUES(@country,@name,@address,@phone_number)",
                    distributor);
                return result;

            }
        }

        public async Task<IEnumerable<Distributor>> getAllIngredientsAndDistributors()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            using (SqlConnection connection = dBConnection.CreateConnection())
            {
                var distributorsDictionary = new Dictionary<int, Distributor>();
                var result = connection.Query<Distributor, Ingredient, Distributor>("GetAllIngredientsAndDistributors",
                    (distributor, ingredient) =>
                    {
                        Distributor distributorEntry;
                        if(!distributorsDictionary.TryGetValue(distributor.distributor_id, out distributorEntry))
                        {
                            distributorEntry = distributor;
                            distributorEntry.ingredients = new List<Ingredient>();
                            distributorsDictionary.Add(distributorEntry.distributor_id, distributorEntry);
                            ingredient.distributor_id = distributorEntry.distributor_id;
                        }                        
                        distributorEntry.ingredients.Add(ingredient);

                        return distributorEntry;
                    }, null, splitOn: "ingredient_id", commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> deleteDistributor(int id)
        {
            var parametrs = new { distributor_id = id };
            using (SqlConnection connection = dBConnection.CreateConnection())
            {
                var result = await connection.ExecuteAsync(
                   "DELETE FROM distributors WHERE distributor_id=@distributor_id", parametrs);
                return result;
            }
        }

        public async Task<IEnumerable<Distributor>> getAllDistributors()
        {
            using(SqlConnection connection = dBConnection.CreateConnection())
            {
                var result = await connection.QueryAsync<Distributor>("SELECT * FROM distributors");
                return result;
            }
        }

        public async Task<Distributor> getDistributor(int id)
        {
            var parameters = new {distributor_id =  id};
            using (SqlConnection connection = dBConnection.CreateConnection())
            {
                var result = await connection.QuerySingleAsync<Distributor>("SELECT * FROM distributors WHERE distributor_id = @distributor_id", parameters);
                return result;
            }
        }

        public async Task<int> updateDistributor(int id, Distributor distributor)
        {
            var parameters = new { distributor_id = id ,
            country = distributor.country,
            name = distributor.name,
            address = distributor.address,
            phone_number = distributor.phone_number};
            using (SqlConnection connection = dBConnection.CreateConnection())
            {
                var result = await connection.ExecuteAsync("UPDATE distributors SET country = @country, name = @name,address = @address, phone_number = @phone_number WHERE distributor_id = @distributor_id", parameters);
                return result;
            }
        }
    }
}
