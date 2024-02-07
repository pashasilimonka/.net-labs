using AutoMapper;
using BakeryADO.Entities;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.Interface.Service;
using DataAccessLayer.Configuration;
using DataAccessLayer.Interface;
using DataAccessLayer.Interface.Repository;
using MassTransit;
using WebApi.Controllers;

namespace WebApi.Consumer
{
    public class ProductConsumer : IConsumer<Product>
    {
        IProductsService _service;

        public ProductConsumer(IProductsService service)
        {
            _service = service;
        }
        public async Task Consume(ConsumeContext<Product> context)
        {
            await Console.Out.WriteAsync($"Received a new product: {context.Message}");
            var product = new ProductRequest
            {
                product_id = context.Message.Id,
                name = context.Message.Name,
                description = context.Message.Description,
                price = context.Message.price.Value
            };
            await _service.InsertAsync(product);

            
        }
    }
}
