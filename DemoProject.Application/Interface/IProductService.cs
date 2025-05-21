using DemoProject.Application.Model;

namespace DemoProject.Application.Interface;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid productId);
    Task<bool> AddProductAsync(Product product);
    Task<Product?> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(Guid id);
    Task<List<Product>> GetFeaturedProductsAsync();
    Task RemoveFeatured(Product product);
    Task<List<Product>> GetProductToRemoveAsync(DateTime time);
}