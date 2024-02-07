using BakeryADO.Entities;
using BakeryADO.Interfaces.Service;
using BakeryADO.Service;

using Dapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BakeryADO.Controllers
{
    [Route("api/distributors")]
    [ApiController]
    public class DistributorsController : ControllerBase
    {
        private readonly IDistributorsService _service;
        private readonly CacheService _cacheService;
        public DistributorsController(IDistributorsService service,CacheService cacheService)
        {
            _service = service;
            _cacheService = cacheService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Distributor>>> getAllDistributors()
        {
            return Ok(await _cacheService.GetOrSet("distributors", () => _service.getAllDistributors(), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(15), 100));
        }

        [HttpGet("{id}")]
        public async  Task<ActionResult<Distributor>> getDistributor(int id)
        {
            return Ok(await _service.getDistributor(id));
        }

        [HttpPost]
        public async Task<ActionResult<int>> createDistributor(Distributor distributor)
        {
            return Ok(await _service.createDistributor(distributor));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> updateDistributor(int id,Distributor distributor)
        {
            return Ok( await _service.updateDistributor(id, distributor));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> deleteDistributor(int id)
        {
            return Ok(await _service.deleteDistributor(id));
        }

        [HttpGet]
        [Route("ingredients_v_distributors")]
        public async Task<IActionResult> getAllIngredientsAndDistributors()
        {
            return Ok(await _service.getAllIngredientsAndDistributors());
        }
    }
}
