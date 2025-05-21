using DemoProject.Application.Model;

namespace DemoProject.Application.Interface;

public interface IProductRepository
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid? id);
    Task AddProductAsync(Product product);
    Task<Product?> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(Product product);
    Task<Product?> GetProductByTitleAsync(string productTitle);
    Task<Product?> GetProductBySkuAsync(string productSku);
    Task<List<Product>> GetAllFeaturedProductsAsync();
    Task<List<Product>> GetProductToRemoveAsync(DateTime time);
}