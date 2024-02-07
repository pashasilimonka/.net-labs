

using AutoMapper;
using BusinesLogicLayer.Interfaces.Service;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces.Repository;

namespace BakeryADO.Service
{
    public class ProductsService:IProductsService
    {
        private readonly IProductsRepository productsRepository;
        private readonly IMapper mapper;
        public ProductsService(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }
        public List<Product> getAllProducts()
        {
            var products = productsRepository.getAllProdutcs();
            return products;
        }

        public Product getProduct(int id) {
            return productsRepository.getProduct(id);
        }

        public int createProduct(Product product)
        {
            return productsRepository.createProduct(product);
        }
        public int updateProduct(int id,string description)
        {
            return productsRepository.updateProduct(id,description);
        }
        public int deleteProduct(int id)
        {
            return productsRepository.deleteProduct(id);
        }
    }
}
