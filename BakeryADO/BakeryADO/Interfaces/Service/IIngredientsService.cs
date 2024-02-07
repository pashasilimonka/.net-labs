

using BakeryADO.Entities;

namespace BakeryADO.Interfaces.Service
{
    public interface IIngredientsService
    {
        Task<IEnumerable<Ingredient>> getAllIngredients();
        Task<Ingredient> getIngredient(int id);
        Task<int> createIngredient(Ingredient ingredient);
        Task<int> updateIngredient(int id, Ingredient ingredient);
        Task<int> deleteIngredient(int id);
        Task<IEnumerable<Ingredient>> GetIngredientsByProduct(int id);
        IEnumerable<ProductIngredients> getAllProductsIngredients();
    }
}
