using DemoProject.Application.Interface;
using DemoProject.Application.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DataEF;

public class CategoryRepositoryEF(AppDbContext context) : ICategoryRepository
{
    public void AddCategory(Category category)
    {
        context.Categories.Add(category);
        context.SaveChanges();
    }

    public Category? GetById(Guid? id)
    {
        return context.Categories.Include(c => c.SubCategories).FirstOrDefault(x => x.Id == id);
    }

    public List<Category> GetAll()
    {
        return context.Categories.ToList();
    }

    public Category? Update(Category category)
    {
        context.SaveChanges();

        return category;
    }

    public bool Delete(Category category)
    {
        context.Categories.Remove(category);
        context.SaveChanges();

        return true;
    }

    public Category? GetCategoryByTitle(string title)
    {
        return context.Categories.FirstOrDefault(x => x.Title == title);
    }

    public Category? GetCategoryByCode(string code)
    {
        return context.Categories.FirstOrDefault(x => x.Code == code);
    }
}