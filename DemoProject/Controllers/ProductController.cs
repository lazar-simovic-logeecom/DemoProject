using AutoMapper;
using DemoProject.Application.Exceptions;
using DemoProject.Application.Interface;
using DemoProject.Application.Model;
using DemoProject.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService, ICategoryService categoryService, IMapper mapper)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        return Ok(await productService.GetProductsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductsByIdAsync(Guid id)
    {
        try
        {
            return Ok(await productService.GetProductByIdAsync(id));
        }
        catch (ModelNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddProductAsync([FromBody] ProductDto dto)
    {
        try
        {
            Product product = mapper.Map<Product>(dto);
            bool isCreated = await productService.AddProductAsync(product);

            if (!isCreated)
            {
                return BadRequest(new { message = "Product not created" });
            }

            return CreatedAtAction("GetProductsById", new { id = product.Id }, product);
        }
        catch (ModelNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ModelAlreadyExistsException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductAsync(Guid id, [FromBody] ProductDto dto)
    {
        try
        {
            Product? product = mapper.Map<Product>(dto);
            product.Id = id;
            Product? updated = await productService.UpdateProductAsync(product);
            if (updated == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }

            return Ok(updated);
        }
        catch (ModelNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(Guid id)
    {
        try
        {
            bool isdeleted = await productService.DeleteProductAsync(id);

            if (!isdeleted)
            {
                return BadRequest(new { message = "Product not deleted" });
            }

            return Ok(new { message = "Product deleted successfully." });
        }
        catch (ModelNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("featured")]
    public async Task<IActionResult> GetFeaturedProductsAsync()
    {
        return Ok(await productService.GetFeaturedProductsAsync());
    }
}