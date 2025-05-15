using DemoProject.Application.Model;

namespace DemoProject.Application.Interface;
public interface ICategoryRepository
{
    Task AddCategoryAsync(Category category);
    Task<Category?> GetByIdAsync(Guid? id);
    Task<List<Category>> GetAllAsync();
    Task<Category?> UpdateAsync(Category category);
    Task<bool> DeleteAsync(Category category);
    Task<Category?> GetCategoryByTitleAsync(String title);
    Task<Category?> GetCategoryByCodeAsync(String code);
    Task<List<Category>> GetCategoryToDelete();
    Task<bool> DeleteHard(Category category);
}