using DemoProject.model;

namespace DemoProject.service;

public class CategoryService
{

    public static List<Category> listCategory = new List<Category>();

    public List<Category> GetAll()
    {
        return listCategory;
    }

    public Category GetById(Guid id)
    {
        var category = listCategory.Find(x => x.Id == id);
        if (category == null) return null;
        return category;
    }

    public void Create(Category category)
    {
        category.Id = Guid.NewGuid();
        listCategory.Add(category);
    }

    public Category? Update(Guid id, Category updatedCategory)
    {
        var existing = listCategory.FirstOrDefault(c => c.Id == id);
        if (existing == null) return null;

        existing.Title = updatedCategory.Title;
        existing.Description = updatedCategory.Description;
        existing.Code = updatedCategory.Code;
        existing.ParentCategory = updatedCategory.ParentCategory;

        return existing;
    }

    public bool Delete(Guid id)
    {
        var category = listCategory.FirstOrDefault(c => c.Id == id);
        if (category == null) return false;

        listCategory.Remove(category);
        return true;
    }
    
}