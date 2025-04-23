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
        
        if (category.ParentCategory != null)
        {
            Category parent = categoryList.FirstOrDefault(c => c.Id == category.ParentCategory);
            if (parent != null)
            {
                parent.SubCategory.Add(category);
            }
        }
    }

    public Category? Update(Guid id, Category updatedCategory)
    {
        Category existing = categoryList.FirstOrDefault(c => c.Id == id);

        if (existing.ParentCategory != updatedCategory.ParentCategory)
        {
            if (existing.ParentCategory != null)
            {
                Category oldParent = categoryList.FirstOrDefault(c => c.Id == existing.ParentCategory);
                oldParent?.SubCategory.Remove(existing);
            }

            if (updatedCategory.ParentCategory != null)
            {
                Category newParent = categoryList.FirstOrDefault(c => c.Id == updatedCategory.ParentCategory);
                newParent?.SubCategory.Add(updatedCategory);
            }
            existing.ParentCategory = updatedCategory.ParentCategory;
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

        if (category.SubCategory != null && category.SubCategory.Count > 0)
        {
            return false;
        }

        if (category.ParentCategory != null)
        {
            Category parent = categoryList.FirstOrDefault(c => c.Id == category.ParentCategory);
            parent?.SubCategory.Remove(category);
        }
        
        categoryList.Remove(category);
        
        return true;
    }
    
}