using DemoProject.Application.Model;

namespace DemoProject.Application.Interface;
public interface ICategoryRepository
{
    void AddCategory(Category category);
    Category? GetById(Guid? id);
    List<Category> GetAll();
    Category? Update(Guid id, Category category);
    bool Delete(Guid id);
    Category? GetCategoryByTitle(String title);
    Category? GetCategoryByCode(String code);
    
}