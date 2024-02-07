using BakeryADO.Entities;
using BakeryADO.Interfaces.Service;
using BakeryADO.Service;

using Dapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace BakeryADO.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientsService _ingredientsService;
        private readonly CacheService _cachedService;
        public IngredientsController(IIngredientsService ingredientsService,CacheService cacheService)
        {
            _ingredientsService = ingredientsService;
            _cachedService = cacheService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK , Type = typeof(List<Ingredient>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("")]
        public async Task<IActionResult> getAllIngredients()
        {
            var result = await _cachedService.GetOrSet("ingredients", () => _ingredientsService.getAllIngredients(), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(15), 100);
            return result == null ? NotFound() : Ok(result);
        }


        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ingredient))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getIngredient(int id)
        {
            if(id <= 0) {  return BadRequest(); }
            var result = await _ingredientsService.getIngredient(id);
            return result.Equals(null) ? NotFound() : Ok(result);
        }

        [HttpGet("product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ingredient))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> getByProduct(int id)
        {
            return Ok(await _ingredientsService.GetIngredientsByProduct(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateIngredient(int id,Ingredient ingredient)
        {
            return Ok(await _ingredientsService.updateIngredient(id, ingredient)); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteIngredient(int id)
        {
            return Ok(await _ingredientsService.deleteIngredient(id));
        }

        [HttpGet("products_ingredients")]
        public IEnumerable<ProductIngredients> getProductIngredients()
        {
            return _ingredientsService.getAllProductsIngredients();
        }
    }
}
