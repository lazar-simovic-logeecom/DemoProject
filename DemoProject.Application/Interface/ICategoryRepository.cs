using DemoProject.Application.Model;

namespace DemoProject.Application.Interface;
public interface ICategoryRepository
{
    Task AddCategoryAsync(Category category);
    Task<Category?> GetByIdAsync(Guid? id);
    Task<List<Category>> GetAllAsync();
    Task<Category?> UpdateAsync(Category category);
    Task<bool> DeleteAsync(Category category);
    Task<Category?> GetCategoryByTitleAsync(string title);
    Task<Category?> GetCategoryByCodeAsync(string code);
    Task<List<Category>> GetCategoryToDelete(DateTime difference);
    Task<bool> DeleteHard(Category category);
}