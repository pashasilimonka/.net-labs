
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Responce;
using BusinessLogicLayer.Interface.Service;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace WebApi.Controllers
{
    [Route("api/productsef")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly IDistributedCache _distributedCache;
        public ProductsController(IProductsService productsService,IDistributedCache distributedCache)
        {
            _productsService = productsService;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<ProductResponce>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> getProducts()
        {
            try
            {
                string key = "products";
                byte[] cachedData = await _distributedCache.GetAsync(key);
                if (cachedData != null)
                {
                    string cachedString = Encoding.UTF8.GetString(cachedData);
                    var cachedProducts = JsonConvert.DeserializeObject<List<ProductResponce>>(cachedString);
                    return Ok(cachedProducts);
                }
                else
                {
                    var products = await _productsService.getAllAsync();
                    var serealizedObjects = JsonConvert.SerializeObject(products);
                    var dataToCache = Encoding.UTF8.GetBytes(serealizedObjects);
                    var cacheOptions = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                        SlidingExpiration = TimeSpan.FromMinutes(10)
                    };
                    await _distributedCache.SetAsync(key, dataToCache, cacheOptions);
                    return Ok(products);
                
                }
                
            }catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new {ex.Message});
            }
        }
        [HttpGet("order/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<ProductResponce>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> getByOrder(int id)
        {
            try
            {
                var result = await _productsService
                    .GetByOrder(id);
                return Ok(result);
            }catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductRequest))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> createProductAsync([FromBody] ProductRequest productRequest)
        {
            try
            {
                await _productsService.InsertAsync(productRequest);
                return Ok(productRequest);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
