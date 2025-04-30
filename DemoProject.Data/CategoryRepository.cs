using DemoProject.Application.Model;
using DemoProject.Application.Interface;

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

    public Category? GetCategoryByTitle(String title)
    {
        return CategoryList.FirstOrDefault(c => c.Title == title);
    }
    
    public Category? GetCategoryByCode(String code)
    {
        return CategoryList.FirstOrDefault(c => c.Code == code);
    }
}