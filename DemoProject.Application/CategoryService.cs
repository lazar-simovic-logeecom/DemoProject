using DemoProject.Application.Exceptions;
using DemoProject.Data.Model;
using DemoProject.Data.Interface;
using DemoProject.Application.Interface;
using DemoProject.Data;

namespace DemoProject.Application
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository = new CategoryRepository();
        
        public List<Category> GetAll()
        {
            return categoryRepository.GetAll();
        }

        public Category? GetById(Guid id)
        {
            Category? category = categoryRepository.GetById(id);
            if (category == null)
            {
                throw new CategoryNotFoundException($"Category with ID {id} not found.");
                
            }
            return category;
        }

        public bool Create(Category category)
        {
            if (!CategoryRepository.SameTitle(category.Title))
            {
                throw new CategoryAlreadyExistsException("Category with the same Title already exists.");
            }

            if (!CategoryRepository.SameCode(category.Code))
            {
                throw new CategoryAlreadyExistsException("Category with the same Code already exists.");
            }

            if (category.ParentCategory.HasValue)
            {
                Category? parent = categoryRepository.GetById(category.ParentCategory);
                if (parent == null)
                {
                    throw new InvalidParentCategoryException("Parent category does not exist.");
                }
                parent.SubCategories.Add(category);
            }

            try
            {
                categoryRepository.AddCategory(category);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the category.", ex);
            }
        }

        public Category? Update(Guid id, Category updatedCategory)
        {
            Category? existingCategory = categoryRepository.GetById(id);
            if (existingCategory == null)
            {
                throw new CategoryNotFoundException($"Category with ID {id} not found.");
            }

            if (existingCategory.ParentCategory != updatedCategory.ParentCategory)
            {
                if (existingCategory.ParentCategory != null)
                {
                    Category? oldParent = categoryRepository.GetById(existingCategory.ParentCategory);
                    oldParent?.SubCategories.Remove(existingCategory);
                }

                if (updatedCategory.ParentCategory != null)
                {
                    Category? newParent = categoryRepository.GetById(updatedCategory.ParentCategory);
                    if (newParent == null)
                    {
                        throw new InvalidParentCategoryException("New parent category does not exist.");
                    }
                    newParent.SubCategories.Add(updatedCategory);
                }

                existingCategory.ParentCategory = updatedCategory.ParentCategory;
            }

            try
            {
                return categoryRepository.Update(id, updatedCategory);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the category.", ex);
            }
        }

        public bool Delete(Guid id)
        {
            Category? existingCategory = categoryRepository.GetById(id);
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
                Category? oldParent = categoryRepository.GetById(existingCategory.ParentCategory.Value);
                oldParent?.SubCategories.Remove(existingCategory);
            }

            try
            {
                return categoryRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the category.", ex);
            }
        }
    }
}
