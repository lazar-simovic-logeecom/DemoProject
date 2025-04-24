using DemoProject.model;

namespace DemoProject.service;

public class CategoryService
{

    private static List<Category> categoryList = new();

    public List<Category> GetAll()
    {
        return categoryList;
    }

    public Category? GetById(Guid id)
    {
        Category? category = categoryList.Find(x => x.Id == id);
        
        return category;
    }

    public void Create(Category category)
    {
        category.Id = Guid.NewGuid();
        categoryList.Add(category);

        if (category.ParentCategory == null)
        {
            return;
        }
        
        Category? parent = categoryList.FirstOrDefault(c => c.Id == category.ParentCategory);
        if (parent == null)
        {
            return;
        }
        
        parent.SubCategories.Add(category);
    }

    public Category? Update(Guid id, Category updatedCategory)
    {
        Category? existingCategory = categoryList.FirstOrDefault(c => c.Id == id);

        if (existingCategory == null)
        {
            return null;
        }
        
        if (existingCategory.ParentCategory != updatedCategory.ParentCategory)
        {
            if (existingCategory.ParentCategory != null)
            {
                Category? oldParent = categoryList.FirstOrDefault(c => c.Id == existingCategory.ParentCategory);
                oldParent?.SubCategories.Remove(existingCategory);
            }

            if (updatedCategory.ParentCategory != null)
            {
                Category? newParent = categoryList.FirstOrDefault(c => c.Id == updatedCategory.ParentCategory);
                newParent?.SubCategories.Add(updatedCategory);
            }
            existingCategory.ParentCategory = updatedCategory.ParentCategory;
        }
        
        existingCategory.Title = updatedCategory.Title;
        existingCategory.Description = updatedCategory.Description;
        existingCategory.Code = updatedCategory.Code;
        existingCategory.ParentCategory = updatedCategory.ParentCategory;

        return existingCategory;
    }

    public bool Delete(Guid id)
    {
        Category? category = categoryList.FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return false;
        }
        
        if (category.SubCategories.Count > 0)
        {
            return false;
        }

        if (category.ParentCategory != null)
        {
            Category? parent = categoryList.FirstOrDefault(c => c.Id == category.ParentCategory);
            parent?.SubCategories.Remove(category);
        }
        
        categoryList.Remove(category);
        
        return true;
    }
    
}