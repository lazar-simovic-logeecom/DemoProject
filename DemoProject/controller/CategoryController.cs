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
    public IActionResult Create(Category category)
    {
        categoryService.addCategory(category);
        return Ok(category);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(categoryService.getAllCategory());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(categoryService.getCategoryById(id));
    }

    /*[HttpPut("{id}")]
    public IActionResult Update(Guid id, Category category)
    {
        
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        
    }*/
    
}