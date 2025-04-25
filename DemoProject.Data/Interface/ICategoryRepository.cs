namespace DemoProject.Data.Interface;
using DemoProject.Domain;
public interface ICategoryRepository
{
    void AddCategory(Category category);
    Category? GetById(Guid? id);
    List<Category> GetAll();
    Category? Update(Guid id, Category category);
    bool Delete(Guid id);
}