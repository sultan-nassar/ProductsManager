using MongoDB.Bson;
using MongoDB.Driver;
using ProductsManager.Models;

namespace ProductsManager.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Product> GetOne(string id);
        Task<Product> EditProduct(string id, Product updatedProduct);
        Task DeleteProduct(string id);
        Task<Product> AddNewProduct(Product p);
        
    }
}
