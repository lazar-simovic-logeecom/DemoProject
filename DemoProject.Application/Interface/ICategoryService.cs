using DemoProject.Domain;

namespace DemoProject.Application;

public interface ICategoryService
{
    List<Category> GetAll();
    Category? GetById(Guid id);
    void Create(Category category);
    Category? Update(Guid id, Category category);
    bool Delete(Guid id);
}