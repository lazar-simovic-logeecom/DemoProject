using DemoProject.Data.Interface;
using DemoProject.Data.Model;

namespace DemoProject.Data;
public class CategoryRepository : ICategoryRepository
{
    private static readonly List<Category> CategoryList = new();
    
    
    public void AddCategory(Category category)
    {
        CategoryList.Add(category);
    }

    public Category? GetById(Guid? id)
    {
        return CategoryList.FirstOrDefault(x => x.Id == id);
    }

    public List<Category> GetAll()
    {
        return CategoryList;
    }

    public Category? Update(Guid id, Category category)
    {
        Category? existingCategory = CategoryList.FirstOrDefault(c => c.Id == id);

        if (existingCategory == null)
        {
            return null;
        }
        existingCategory.Update(category);
        
        return existingCategory;
    }

    public bool Delete(Guid id)
    {
        Category? existingCategory = CategoryList.FirstOrDefault(c => c.Id == id);

        if (existingCategory == null)
        {
            return false;
        }
        CategoryList.Remove(existingCategory);
        
        return true;
    }

    public static bool SameTitle(String title)
    {
        if (CategoryList.Any(c => c.Title == title))
        {
            return true;
        }
        
        return false;
    }
    
    public static bool SameCode(String code)
    {
        if (CategoryList.Any(c => c.Code == code))
        {
            return true;
        }
        
        return false;
    }
}