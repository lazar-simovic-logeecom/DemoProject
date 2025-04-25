using DemoProject.Data.Interface;
using DemoProject.Domain;

namespace DemoProject.Data;
public class CategoryRepository : ICategoryRepository
{
    private static List<Category> categoryList = new();
    
    public void AddCategroy(Category category)
    {
        categoryList.Add(category);
    }

    public Category? GetById(Guid? id)
    {
        return categoryList.FirstOrDefault(x => x.Id == id);
    }

    public List<Category> GetAll()
    {
        return categoryList;
    }

    public Category? Update(Guid id, Category category)
    {
        Category? existingCategory = categoryList.FirstOrDefault(c => c.Id == id);

        if (existingCategory == null)
        {
            return null;
        }
        
        existingCategory.Title = category.Title;
        existingCategory.Description = category.Description;
        existingCategory.Code = category.Code;

        return existingCategory;
    }

    public bool Delete(Guid id)
    {
        Category? existingCategory = categoryList.FirstOrDefault(c => c.Id == id);

        if (existingCategory == null)
        {
            return false;
        }
                    
        categoryList.Remove(existingCategory);
        return true;
    }
}