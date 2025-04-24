using AutoMapper;
using DemoProject.Dto;
using DemoProject.model;
using DemoProject.service;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.controller;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService categoryService;
    private readonly IMapper mapper;
    public CategoryController(CategoryService categoryService, IMapper mapper)
    {
        this.categoryService = categoryService;
        this.mapper = mapper;
    }
    
    [HttpPost]
    public IActionResult AddCategory([FromBody] CategoryDto dto)
    {
        bool sameTitle = categoryService.GetAll().Any(c => c.Title == dto.Title);
        if (sameTitle)
        {
            return BadRequest("Category with the same Title already exists.");
        }
        
        bool sameCode = categoryService.GetAll().Any(c => c.Code == dto.Code);
        if (sameCode)
        {
            return BadRequest("Category with the same Code already exists.");
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        Category category = mapper.Map<Category>(dto);
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
        Category? category = categoryService.GetById(id);
        if (category == null)
        {
            return NotFound("Category not found.");
        }

        return Ok(category);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(Guid id, [FromBody] CategoryDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Category updatedCategory = mapper.Map<Category>(dto);
        Category? result = categoryService.Update(id, updatedCategory);
        if (result == null)
        {
            return NotFound("Category not found.");
        }
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(Guid id)
    {
        Category? existingCategory = categoryService.GetById(id);
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