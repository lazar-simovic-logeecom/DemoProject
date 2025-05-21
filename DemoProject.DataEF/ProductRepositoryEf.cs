using DemoProject.Application.Interface;
using DemoProject.Application.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DataEF;

public class ProductRepositoryEf(AppDbContext context) : IProductRepository
{
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid? id)
    {
        return await context.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddProductAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task<Product?> UpdateProductAsync(Product product)
    {
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(Product product)
    {
        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<Product?> GetProductByTitleAsync(string productTitle)
    {
        return await context.Products.FirstOrDefaultAsync(x => x.Title == productTitle);
    }

    public async Task<Product?> GetProductBySkuAsync(string productSku)
    {
        return await context.Products.FirstOrDefaultAsync(x => x.Sku == productSku);
    }

    public async Task<List<Product>> GetAllFeaturedProductsAsync()
    {
        return await context.Products
            .Where(p => p.GetTime != null)
            .ToListAsync();
    }

    public async Task<List<Product>> GetProductToRemoveAsync(DateTime time)
    {
        return await context.Products
            .Where(p => p.GetTime <= time)
            .ToListAsync();
    }
}