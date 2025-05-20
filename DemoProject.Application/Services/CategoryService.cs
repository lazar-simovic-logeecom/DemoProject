using DemoProject.Application.Exceptions;
using DemoProject.Application.Model;
using DemoProject.Application.Interface;

namespace DemoProject.Application
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        public async Task<List<Category>> GetAllAsync()
        {
            return await categoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            Category? category = await categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                throw new CategoryNotFoundException($"Category with ID {id} not found.");
            }

            return category;
        }

        public async Task<bool> CreateAsync(Category category)
        {
            if (await categoryRepository.GetCategoryByTitleAsync(category.Title) != null)
            {
                throw new CategoryAlreadyExistsException("Category with the same Title already exists.");
            }

            if (await categoryRepository.GetCategoryByCodeAsync(category.Code) != null)
            {
                throw new CategoryAlreadyExistsException("Category with the same Code already exists.");
            }

            if (category.ParentCategory.HasValue)
            {
                if (category.ParentCategory == category.Id)
                {
                    throw new InvalidParentCategoryException("Category cannot be its own parent.");
                }

                Category? parent = await categoryRepository.GetByIdAsync(category.ParentCategory.Value);
                if (parent == null)
                {
                    throw new CategoryNotFoundException("Parent category not found.");
                }

                parent.SubCategories.Add(category);
            }

            await categoryRepository.AddCategoryAsync(category);

            return true;
        }

        public async Task<Category?> UpdateAsync(Category updatedCategory)
        {
            Category? existingCategory = await categoryRepository.GetByIdAsync(updatedCategory.Id);
            if (existingCategory == null)
            {
                throw new CategoryNotFoundException($"Category with ID {updatedCategory.Id} not found.");
            }

            if (updatedCategory.ParentCategory == updatedCategory.Id)
            {
                throw new InvalidParentCategoryException("A category cannot be its own parent.");
            }

            if (existingCategory.DeletedAt.HasValue)
            {
                throw new CategoryHasBeenDeletedException("Category with ID " + updatedCategory.Id + " has been deleted."); 
            }
            
            existingCategory.Update(updatedCategory);

            if (existingCategory.ParentCategory != updatedCategory.ParentCategory)
            {
                if (existingCategory.ParentCategory.HasValue)
                {
                    Category? oldParent = await categoryRepository.GetByIdAsync(existingCategory.ParentCategory.Value);
                    oldParent?.SubCategories.Remove(existingCategory);
                }

                if (updatedCategory.ParentCategory.HasValue)
                {
                    Category? newParent = await categoryRepository.GetByIdAsync(updatedCategory.ParentCategory.Value);
                    if (newParent == null)
                    {
                        throw new InvalidParentCategoryException("New parent category does not exist.");
                    }

                    newParent.SubCategories.Add(updatedCategory);
                }

                existingCategory.ParentCategory = updatedCategory.ParentCategory;
            }

            return await categoryRepository.UpdateAsync(existingCategory);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Category? existingCategory = await categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                throw new CategoryNotFoundException($"Category with ID {id} not found.");
            }

            if (existingCategory.SubCategories.Any())
            {
                throw new CategoryHasSubCategoriesException("Cannot delete category with subcategories.");
            }

            if (existingCategory.ParentCategory.HasValue)
            {
                Category? oldParent = await categoryRepository.GetByIdAsync(existingCategory.ParentCategory.Value);
                oldParent?.SubCategories.Remove(existingCategory);
            }

            return await categoryRepository.DeleteAsync(existingCategory);
        }
    }
}