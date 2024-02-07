

using DataAccessLayer.Configuration;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces.Repository;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DBConnection dBConnection;
        public ProductsRepository(DBConnection connection)
        {
            dBConnection = connection;
        }
        public  List<Product> getAllProdutcs()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection sqlConnection = dBConnection.CreateConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM products", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.Id = reader.GetInt32(0);
                        product.Name = reader.GetString(1);
                        product.Description = reader.GetString(2);
                        product.recipe = reader.GetString(3);
                        product.price = reader.GetDouble(4);
                        products.Add(product);
                    }
                }
            }

            return products;
        }
        public Product getProduct(int id)
        {
            Product product = new Product();
            using (SqlConnection sqlConnection = dBConnection.CreateConnection())
            {

                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM products WHERE product_id = @id", sqlConnection);
                SqlParameter parameter = new SqlParameter("@id", id);
                cmd.Parameters.Add(parameter);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        product.Id = reader.GetInt32(0);
                        product.Name = reader.GetString(1);
                        product.Description = reader.GetString(2);
                        product.recipe = reader.GetString(3);
                        product.price = reader.GetDouble(4);
                    }
                }
            }

            return product;
        }

        public int createProduct(Product product)
        {
            int status;
            using (SqlConnection sqlConnection = dBConnection.CreateConnection())
            {

                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO products (name, description, recipe, price) VALUES (@name,@description,@recipe,@price)", sqlConnection);
                SqlParameter parameterName = new SqlParameter("@name", product.Name);
                cmd.Parameters.Add(parameterName);
                SqlParameter parameterDescription = new SqlParameter("@description", product.Description);
                cmd.Parameters.Add(parameterDescription);
                SqlParameter parameterRecipe = new SqlParameter("@recipe", product.recipe);
                cmd.Parameters.Add(parameterRecipe);
                SqlParameter parameterPrice = new SqlParameter("@price", product.price);
                cmd.Parameters.Add(parameterPrice);
                status = cmd.ExecuteNonQuery();
            }

            return status;
        }
        public int updateProduct(int id, string description)
        {
            int status;
            using (SqlConnection sqlConnection = dBConnection.CreateConnection())
            {

                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("UpdateProduct", sqlConnection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter parameterName = new SqlParameter("@Description", description);
                SqlParameter parameterId = new SqlParameter("@Id", id);
                cmd.Parameters.Add(parameterName);
                cmd.Parameters.Add(parameterId);
                status = cmd.ExecuteNonQuery();
            }

            return status;
        }
        public int deleteProduct(int id)
        {
            int status;
            using (SqlConnection sqlConnection = dBConnection.CreateConnection())
            {

                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM products WHERE product_id=@id", sqlConnection);
                SqlParameter parameter = new SqlParameter("@id", id);
                cmd.Parameters.Add(parameter);
                status = cmd.ExecuteNonQuery();
            }

            return status;
        }
    }
}
