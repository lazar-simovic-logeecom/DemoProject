using AutoMapper;
using DemoProject.Application;
using DemoProject.Application.Interface;
using DemoProject.Dto;
using DemoProject.Data.Model;
using DemoProject.Application.Exceptions;
using DemoProject.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService = new CategoryService(); 
        private readonly IMapper mapper;

        public CategoryController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            mapper = config.CreateMapper();
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryDto dto)
        {
            try
            {
                Category category = mapper.Map<Category>(dto);
                bool isCreated = categoryService.Create(category);
                if (!isCreated)
                {
                    return BadRequest(new { message = "Failed to create category." });
                }

                return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
            }
            catch (CategoryAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (InvalidParentCategoryException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(categoryService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                Category? category = categoryService.GetById(id);
                return Ok(category);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(Guid id, [FromBody] CategoryDto dto)
        {
            try
            {
                Category updatedCategory = mapper.Map<Category>(dto);
                Category? updated = categoryService.Update(id, updatedCategory);

                if (updated == null)
                {
                    return NotFound(new { message = $"Category with ID {id} not found." });
                }

                return Ok(updated);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidParentCategoryException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(Guid id)
        {
            try
            {
                bool isDeleted = categoryService.Delete(id);
                if (!isDeleted)
                {
                    return NotFound(new { message = "Category not found or cannot be deleted due to subcategories." });
                }

                return Ok(new { message = "Category deleted successfully." });
            }
            catch (CategoryHasSubCategoriesException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }
    }
}
