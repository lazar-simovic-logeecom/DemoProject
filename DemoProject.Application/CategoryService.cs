using DemoProject.Data;
using DemoProject.Data.Interface;

namespace DemoProject.Application;
using DemoProject.Domain;
public class CategoryService : ICategoryService
{
    private ICategoryRepository categoryRepository;

    public CategoryService()
    {
        categoryRepository = new CategoryRepository();
    }

    public List<Category> GetAll()
    {
        return categoryRepository.GetAll();
    }

    public Category? GetById(Guid id)
    {
        Category? category = categoryRepository.GetById(id);
        
        return category;
    }

    public bool Create(Category category)
    {
        if (category.ParentCategory.HasValue)
        {
            Category? parent = categoryRepository.GetById(category.ParentCategory);
            if (parent == null)
            {
                return false;
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
            return null;
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
                newParent?.SubCategories.Add(updatedCategory);
            }
            existingCategory.ParentCategory = updatedCategory.ParentCategory;
        }
        
        return categoryRepository.Update(id, updatedCategory);
    }

    public bool Delete(Guid id)
    {
        Category? existingCategory = categoryRepository.GetById(id);

        if (existingCategory == null || existingCategory.SubCategories.Any())
        {
            return false;
        }

        if (existingCategory.ParentCategory.HasValue)
        {
            Category? oldParent = categoryRepository.GetById(existingCategory.ParentCategory.Value);
            oldParent?.SubCategories.Remove(existingCategory);
        }

        return categoryRepository.Delete(id);
    }
}