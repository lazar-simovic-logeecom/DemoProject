using DemoProject.model;

namespace DemoProject.service;

public class CategoryService
{

    public static List<Category> listCategory = new List<Category>();

    public List<Category> getAllCategory()
    {
        return listCategory;
    }

    public Category getCategoryById(Guid id)
    {
        return listCategory.FirstOrDefault(x => x.Id == id);
    }

    public void addCategory(Category category)
    {
        category.Id = Guid.NewGuid();
        listCategory.Add(category);
    }

    public void updateCategory(Category category)
    {
        listCategory.Remove(category);
    }

    public void deleteCategory(Category category)
    {
        listCategory.Remove(category);
    }
    
}