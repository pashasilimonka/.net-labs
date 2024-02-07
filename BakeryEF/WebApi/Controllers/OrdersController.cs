using BusinessLogicLayer.DTO.Responce;
using BusinessLogicLayer.Interface.Service;
using BusinessLogicLayer.Service;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Pagination;
using DataAccessLayer.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly IDistributedCache _distributedCache;
        public OrdersController(IOrdersService ordersService, IDistributedCache distributedCache)
        {
            _ordersService = ordersService;
            _distributedCache = distributedCache;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<OrderResponce>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> getOrders()
        {
            try
            {
                string key = "orders";
                byte[] cachedData = await _distributedCache.GetAsync(key);
                if (cachedData != null)
                {
                    string cachedString = Encoding.UTF8.GetString(cachedData);
                    var cachedProducts = JsonConvert.DeserializeObject<List<ProductResponce>>(cachedString);
                    return Ok(cachedProducts);
                }
                else
                {
                    var products = await _ordersService.GetAsync();
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
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderResponce))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getOrder(int id)
        {
            if (id <= 0) return BadRequest(id);
            try
            {
                var result = await _ordersService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }

        }

        [HttpGet("product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderProductResponce))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getOrderProduct(int id)
        {
            if (id <= 0) return BadRequest(id);
            try
            {
                var result = await _ordersService.GetCompleteByIdAsync(id);
                return Ok(result);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }

        }
        [HttpGet("product")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderProductResponce>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getOrderProduct()
        {
            try
            {
                var result = await _ordersService.GetCompleteByIdAsync();
                return Ok(result);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }

        }
        [HttpGet("client/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderResponce))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByClientIdAsync(int id)
        {
            if (id <= 0) return BadRequest(id);
            try
            {
                var result = await _ordersService.GetByClientIdAsync(id);
                return Ok(result);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }
    }
}
