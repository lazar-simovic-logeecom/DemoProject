using AutoMapper;
using DemoProject.Dto;
using DemoProject.Application.Model;
using DemoProject.Application.Exceptions;
using DemoProject.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(ICategoryService categoryService, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CategoryDto dto)
        {
            try
            {
                Category category = mapper.Map<Category>(dto);
                bool isCreated = await categoryService.CreateAsync(category);
                if (!isCreated)
                {
                    return BadRequest(new { message = "Failed to create category." });
                }

                return CreatedAtAction("GetById", new { id = category.Id }, category);
            }
            catch (ModelAlreadyExistsException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidParentCategoryException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await categoryService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                Category? category = await categoryService.GetByIdAsync(id);

                return Ok(category);
            }
            catch (ModelNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(Guid id, [FromBody] CategoryDto dto)
        {
            try
            {
                Category updatedCategory = mapper.Map<Category>(dto);
                updatedCategory.Id = id;
                Category? updated = await categoryService.UpdateAsync(updatedCategory);

                if (updated == null)
                {
                    return NotFound(new { message = $"Category with ID {id} not found." });
                }

                return Ok(updated);
            }
            catch (ModelNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidParentCategoryException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (CategoryHasBeenDeletedException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(Guid id)
        {
            try
            {
                bool isDeleted = await categoryService.DeleteAsync(id);
                if (!isDeleted)
                {
                    return NotFound(new { message = "Category not found or cannot be deleted due to subcategories." });
                }

                return Ok(new { message = "Category deleted successfully." });
            }
            catch (CategoryHasSubCategoriesException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ModelNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}