using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Responce;
using BusinessLogicLayer.Interface.Service;
using BusinessLogicLayer.Service;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsConroller : ControllerBase
    {
        private readonly IClientsService _clientsService;
        private readonly IDistributedCache _distributedCache;
        public ClientsConroller(IClientsService clientsService,IDistributedCache cacheService)
        {
            _clientsService = clientsService;
            _distributedCache = cacheService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(PagedList<ClientResponce>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getClients()
        {
            try
            {

                string key = "clients";
                byte[] cachedData = await _distributedCache.GetAsync(key);
                if (cachedData != null)
                {
                    string cachedString = Encoding.UTF8.GetString(cachedData);
                    var cachedProducts = JsonConvert.DeserializeObject<List<ClientResponce>>(cachedString);
                    return Ok(cachedProducts);
                }
                else
                {
                    var products = await _clientsService.GetAsync();
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponce))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getClient(int id)
        {
            if (id <= 0) return BadRequest(id);
            try
            {
                var result = await _clientsService.GetByIdAsync(id);
                return Ok(result);
            }catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
            
        }
        [HttpGet("orders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponce))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getAllWithOrders()
        {
            try
            {
                var result = await _clientsService.GetCompleteByIdAsync();
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }
        [HttpGet("orders/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponce))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getAllWithOrders(int id)
        {
            try
            {
                var result = await _clientsService.GetCompleteByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddClient(ClientRequest request)
        {
            try
            {
                await _clientsService.InsertAsync(request);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateClient(ClientRequest request)
        {
            try
            {
                await _clientsService.UpdateAsync(request);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteClient(int id) {
            if (id <= 0) return BadRequest();
            try
            {
                await _clientsService.DeleteAsync(id);
                return Ok();
            }catch(EntityNotFoundException ex)
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
