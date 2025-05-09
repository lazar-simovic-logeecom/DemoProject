using DemoProject.Application.Model;
using DemoProject.Application.Interface;

namespace DemoProject.Data;

public class CategoryRepository : ICategoryRepository
{
    private readonly List<Category> CategoryList = new();


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

    public Category? Update(Category category)
    {
        Category? existingCategory = CategoryList.FirstOrDefault(c => c.Id == category.Id);

        if (existingCategory == null)
        {
            return null;
        }

        existingCategory.Update(category);

        return existingCategory;
    }

    public bool Delete(Category category)
    {
        CategoryList.Remove(category);

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