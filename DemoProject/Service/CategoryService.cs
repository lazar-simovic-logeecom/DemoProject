using DemoProject.model;

namespace DemoProject.service;

public class CategoryService
{

    private static List<Category> categoryList = new List<Category>();

    public List<Category> GetAll()
    {
        return categoryList;
    }

    public Category? GetById(Guid id)
    {
        Category category = categoryList.Find(x => x.Id == id);
        if (category == null)
        {
            return null;
        }
        
        return category;
    }

    public void Create(Category category)
    {
        category.Id = Guid.NewGuid();
        categoryList.Add(category);
    }

    public Category? Update(Guid id, Category updatedCategory)
    {
        Category existing = categoryList.FirstOrDefault(c => c.Id == id);
        if (existing == null)
        {
            return null;
        }

        existing.Title = updatedCategory.Title;
        existing.Description = updatedCategory.Description;
        existing.Code = updatedCategory.Code;
        existing.ParentCategory = updatedCategory.ParentCategory;

        return existing;
    }

    public bool Delete(Guid id)
    {
        Category category = categoryList.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return false;
        }

        categoryList.Remove(category);
        
        return true;
    }
    
}