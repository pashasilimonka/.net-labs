

using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces.Repository
{
    public interface IIngredientsRepository
    {
        Task<IEnumerable<Ingredient>> getAllIngredients();
        Task<Ingredient> getIngredient(int id);
        Task<int> createIngredient(Ingredient ingredient);
        Task<int> updateIngredient(int id, Ingredient ingredient);
        Task<int> deleteIngredient(int id);
        IEnumerable<ProductIngredients> getAllProductsIngredients();
        Task<IEnumerable<Ingredient>> getIngredientsByProduct(int id);
    }
}
