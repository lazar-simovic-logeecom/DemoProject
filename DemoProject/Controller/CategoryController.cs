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

        bool sameTitle = categoryService.GetAll().Any(c => c.Title == category.Title);
        if (sameTitle)
        {
            return BadRequest("Category with the same Title already exists.");
        }
        
        bool sameCode = categoryService.GetAll().Any(c => c.Code == category.Code);
        if (sameCode)
        {
            return BadRequest("Category with the same Code already exists.");
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
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
            return NotFound("Category not found.");
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
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        Category result = categoryService.Update(id, updatedCategory);
        if (result == null)
        {
            return NotFound("Update failed: Category could not be updated.");
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
            return BadRequest("Cannot delete category that has subcategories.");
        }

        return NoContent();
    }
    
}