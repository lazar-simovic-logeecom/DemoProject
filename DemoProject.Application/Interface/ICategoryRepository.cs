using DemoProject.Application.Model;

namespace DemoProject.Application.Interface;
public interface ICategoryRepository
{
    void AddCategory(Category category);
    Category? GetById(Guid? id);
    List<Category> GetAll();
    Category? Update(Category category);
    bool Delete(Category category);
    Category? GetCategoryByTitle(String title);
    Category? GetCategoryByCode(String code);
    
}