using DemoProject.Application.Exceptions;
using DemoProject.Application.Model;
using DemoProject.Application.Interface;

namespace DemoProject.Application
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository = categoryRepository;

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
            if (categoryRepository.GetCategoryByTitle(category.Title) != null)
            {
                throw new CategoryAlreadyExistsException("Category with the same Title already exists.");
            }

            if (categoryRepository.GetCategoryByCode(category.Code) != null)
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
            categoryRepository.AddCategory(category);

            return true;
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

            return categoryRepository.Update(id, updatedCategory);
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

            return categoryRepository.Delete(id);
        }
    }
}