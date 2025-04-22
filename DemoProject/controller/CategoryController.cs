using DemoProject.model;
using DemoProject.service;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.controller;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{

    public readonly CategoryService categoryService;

    public CategoryController(CategoryService categoryService)
    {
        this.categoryService = categoryService;
    }
    
    [HttpPost]
    public IActionResult AddCategory([FromBody] Category category)
    {
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
        var category = categoryService.GetById(id);
        if (category == null)
            return NotFound();

        return Ok(category);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(Guid id, [FromBody] Category updatedCategory)
    {
        var result = categoryService.Update(id, updatedCategory);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(Guid id)
    {
        bool deleted = categoryService.Delete(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
    
}