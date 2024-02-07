
using BakeryADO.Entities;
using BakeryADO.Interfaces.Service;
using BakeryADO.Service;

using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BakeryADO.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly CacheService _cacheService;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductsController(IProductsService productsService, CacheService service, IPublishEndpoint publishEndpoint)
        {
            _productsService = productsService;
            _cacheService = service;
            _publishEndpoint = publishEndpoint;
        }
        [HttpGet]
        [Route("")]
        public List<Product> getAllProducts()
        {

            return _cacheService.GetOrSet("products", ()=>_productsService.getAllProducts(), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(15), 100);
        }
        [HttpGet]
        [Route("{id}")]
        public Product getProduct(int id)
        {
           return _productsService.getProduct(id);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> createProduct(Product product)
        {
            await _publishEndpoint.Publish<Product>(product);
            return Ok(_productsService.createProduct(product));
            
        }

        [HttpPut]
        [Route("{id}")]
        public int updateProduct(int id,string description)
        {
            return _productsService.updateProduct(id, description);
        }

        [HttpDelete]
        [Route("{id}")]
        public int deleteProduct(int id) {
            return _productsService.deleteProduct(id);
        }

    }
}
