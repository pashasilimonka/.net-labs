
using BakeryADO.Configuration;
using BakeryADO.Entities;
using BakeryADO.Interfaces.Repository;
using Dapper;

using Microsoft.Data.SqlClient;

using System.Data;

namespace BakeryADO.Repository
{
    public class IngredientsRepository : IIngredientsRepository
    {
        private readonly DBConnection dBConnection;

        public  IngredientsRepository(DBConnection connection)
        {
            dBConnection = connection;
        }
        public async Task<int> createIngredient(Ingredient ingredient)
        {

            using (SqlConnection connection = dBConnection.CreateConnection())
            {
                var result = await connection.ExecuteAsync("INSERT INTO ingredients(name,description,distributor_id) VALUES (@name,@description,@distributor_id)",ingredient);
                return result;
            }
        }

        public async Task<int> deleteIngredient(int id)
        {
            var parametrs = new { ingredient_id = id };
            using (SqlConnection connection = dBConnection.CreateConnection())
            {
                var result = await connection.ExecuteAsync(
                   "DELETE FROM ingredients WHERE ingredient_id=@ingredient_id", parametrs);
                return result;
            }
        }

        public async Task<IEnumerable<Ingredient>> getAllIngredients()
        {
            using (SqlConnection connection = dBConnection.CreateConnection())
            {
                var result = await connection.QueryAsync<Ingredient>("SELECT ingredient_id,name,description FROM ingredients");

                return result;
            }
        }

        public  IEnumerable<ProductIngredients> getAllProductsIngredients()
        {
            using (SqlConnection connection = dBConnection.CreateConnection())
            {
                var dict = new Dictionary<int,ProductIngredients>();
                var result = connection.Query<ProductIngredients, Ingredient, ProductIngredients>("GetAllProductsAndIngredients",
                    (product, ingredient) =>
                    {
                        if(!dict.TryGetValue(product.product_id,out ProductIngredients prod)){
                            prod = product;
                            prod.ingredient_id = new List<Ingredient>();
                            dict.Add(product.product_id, prod);
                        }
                        if (ingredient !=null)
                        {
                            prod.ingredient_id.Add(ingredient);
                        }
                        return prod;
                    },splitOn:"ingredient_id",commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<Ingredient> getIngredient(int id)
        {
            var parametrs = new { ingredient_id = id };
            using (SqlConnection connection = dBConnection.CreateConnection())
            {
                var result = await connection.QuerySingleAsync<Ingredient>(
                    "SELECT ingredient_id,name,description FROM ingredients WHERE ingredient_id=@ingredient_id", parametrs);
                return result;
            }
        }

        public async Task<IEnumerable<Ingredient>> getIngredientsByProduct(int id)
        {
            var parametrs = new { product_id = id };
            using (SqlConnection connection = dBConnection.CreateConnection())
            {
                var ingredients = await connection.QueryAsync<Ingredient>("SELECT i.* " +
                    "FROM ingredients i JOIN ingredients_products pi ON i.ingredient_id = pi.ingredient_id JOIN products p ON pi.product_id = p.product_id WHERE p.product_id = @product_id", parametrs);
                return ingredients;
            }
        }

        public async Task<int> updateIngredient(int id, Ingredient ingredient)
        {
            var parameters = new
            {
                name = ingredient.name,
                description = ingredient.description,
                ingredient_id = id,
            };
            using (SqlConnection connection = dBConnection.CreateConnection())
            {
                var result = await connection.ExecuteAsync("UPDATE ingredients SET name = @name, description = @description WHERE ingredient_id = @ingredient_id", parameters);
                return result;
            }
        }


    }
}
