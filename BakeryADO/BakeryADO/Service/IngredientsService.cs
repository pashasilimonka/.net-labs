using BakeryADO.Entities;
using BakeryADO.Interfaces.Repository;
using BakeryADO.Interfaces.Service;


namespace BakeryADO.Service
{
    public class IngredientsService : IIngredientsService

    {
        private readonly IIngredientsRepository _repository;
        public IngredientsService(IIngredientsRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> createIngredient(Ingredient ingredient)
        {
            return await _repository.createIngredient(ingredient);
        }

        public async Task<int> deleteIngredient(int id)
        {
            return await _repository.deleteIngredient(id);
        }

        public async Task<IEnumerable<Ingredient>> getAllIngredients()
        {
            return await _repository.getAllIngredients();
        }

        public IEnumerable<ProductIngredients> getAllProductsIngredients()
        {
            return _repository.getAllProductsIngredients();
        }

        public async Task<Ingredient> getIngredient(int id)
        {
            return await _repository.getIngredient(id); 
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByProduct(int id)
        {
            return await _repository.getIngredientsByProduct(id);
        }

        public async Task<int> updateIngredient(int id, Ingredient ingredient)
        {
            return await _repository.updateIngredient(id, ingredient);
        }

      
    }
}
