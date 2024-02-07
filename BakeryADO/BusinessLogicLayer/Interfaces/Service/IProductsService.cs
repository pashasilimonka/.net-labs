﻿

using DataAccessLayer.Entities;

namespace BusinesLogicLayer.Interfaces.Service
{
    public interface IProductsService
    {
        List<Product> getAllProducts();
        Product getProduct(int id);
        int createProduct(Product product);
        int updateProduct(int id, string description);
        int deleteProduct(int id);
    }
}
