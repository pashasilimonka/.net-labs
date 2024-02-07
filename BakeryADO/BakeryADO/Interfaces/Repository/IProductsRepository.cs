


using BakeryADO.Entities;

namespace BakeryADO.Interfaces.Repository
{
    public interface IProductsRepository
    {
        List<Product> getAllProdutcs();
        Product getProduct(int id);
        int createProduct(Product product);
        int updateProduct(int id,string description);
        int deleteProduct(int id);

    }
}
