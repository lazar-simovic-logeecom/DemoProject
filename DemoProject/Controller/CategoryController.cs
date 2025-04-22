using DemoProject.model;
using DemoProject.service;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.controller;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{

    private readonly CategoryService categoryService;

    public CategoryController(CategoryService categoryService)
    {
        this.categoryService = categoryService;
    }
    
    [HttpPost]
    public IActionResult AddCategory([FromBody] Category category)
    {
        Category existingCategory = categoryService.GetById(category.Id);
        if (existingCategory != null)
        {
            return BadRequest("Category with the same ID already exists.");
        }
        categoryService.Create(category);
        
        return Ok(category);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(categoryService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        Category category = categoryService.GetById(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(Guid id, [FromBody] Category updatedCategory)
    {
        Category existingCategory = categoryService.GetById(id);
        if (existingCategory == null)
        {
            return NotFound("Category not found.");
        }
        
        Category result = categoryService.Update(id, updatedCategory);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(Guid id)
    {
        Category existingCategory = categoryService.GetById(id);
        if (existingCategory == null)
        {
            return NotFound("Category not found.");
        }
        
        bool deleted = categoryService.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
    
}