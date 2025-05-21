using DemoProject.Application.Exceptions;
using DemoProject.Application.Interface;
using DemoProject.Application.Model;

namespace DemoProject.Application.Services;

public class ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
    : IProductService
{
    public async Task<List<Product>> GetProductsAsync()
    {
        return await productRepository.GetAllProductsAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid productId)
    {
        Product? product = await productRepository.GetProductByIdAsync(productId);
        if (product == null)
        {
            throw new ModelNotFoundException("Product not found");
        }
        product.GetTime = DateTime.UtcNow;
        Product? newProduct = await productRepository.UpdateProductAsync(product);
        
        return newProduct;
    }

    public async Task<bool> AddProductAsync(Product product)
    {
        if (await productRepository.GetProductByTitleAsync(product.Title) != null)
        {
            throw new ModelAlreadyExistsException("Product with the same Title already exists.");
        }

        if (await productRepository.GetProductBySkuAsync(product.Sku) != null)
        {
            throw new ModelAlreadyExistsException("Category with the same Sku already exists.");
        }

        if (product.CategoryId.HasValue)
        {
            Category? parent = await categoryRepository.GetByIdAsync(product.CategoryId);
            if (parent == null)
            {
                throw new ModelNotFoundException("Product category not found.");
            }

            parent.Products.Add(product);
        }

        await productRepository.AddProductAsync(product);

        return true;
    }

    public async Task<Product?> UpdateProductAsync(Product product)
    {
        Product? productToUpdate = await productRepository.GetProductByIdAsync(product.Id);
        if (productToUpdate == null)
        {
            throw new ModelNotFoundException("Product not found");
        }

        productToUpdate.Update(product);

        if (productToUpdate.CategoryId != product.CategoryId)
        {
            if (productToUpdate.CategoryId.HasValue)
            {
                Category? parent = await categoryRepository.GetByIdAsync(productToUpdate.CategoryId.Value);
                parent?.Products.Remove(productToUpdate);
            }

            if (product.CategoryId.HasValue)
            {
                Category? newParent = await categoryRepository.GetByIdAsync(product.CategoryId.Value);
                if (newParent == null)
                {
                    throw new ModelNotFoundException("Product category not found.");
                }

                newParent.Products.Add(productToUpdate);
            }

            productToUpdate.CategoryId = product.CategoryId;
        }

        return await productRepository.UpdateProductAsync(productToUpdate);
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        Product? product = await GetProductByIdAsync(id);
        if (product == null)
        {
            throw new ModelNotFoundException("Product not found");
        }

        if (product.CategoryId.HasValue)
        {
            Category? parent = await categoryRepository.GetByIdAsync(product.CategoryId.Value);
            if (parent == null)
            {
                throw new ModelNotFoundException("Product category not found.");
            }

            parent.Products.Remove(product);
        }

        await productRepository.DeleteProductAsync(product);

        return true;
    }

    public async Task<List<Product>> GetFeaturedProductsAsync()
    {
        return await productRepository.GetAllFeaturedProductsAsync();
    }

    public async Task RemoveFeatured(Product product)
    {
        product.GetTime = null;
        await productRepository.UpdateProductAsync(product);
    }

    public async Task<List<Product>> GetProductToRemoveAsync(DateTime time)
    {
        return await productRepository.GetProductToRemoveAsync(time);
    }
}