using DemoProject.Application.Model;
using DemoProject.Application.Interface;

namespace DemoProject.Data;

public class CategoryRepository : ICategoryRepository
{
    private readonly List<Category> CategoryList = new();


    public async Task AddCategoryAsync(Category category)
    {
        await Task.Run(() => CategoryList.Add(category));
    }

    public async Task<Category?> GetByIdAsync(Guid? id)
    {
        return await Task.Run(() => CategoryList.FirstOrDefault(x => x.Id == id));
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await Task.Run(() => CategoryList.ToList());
    }

    public async Task<Category?> UpdateAsync(Category category)
    {
        return await Task.Run(() =>
        {
            Category? existingCategory = CategoryList.FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory == null) return null;
            existingCategory.Update(category);
            return existingCategory;
        });
    }

    public async Task<bool> DeleteAsync(Category category)
    {
        return await Task.Run(() =>
        {
            CategoryList.Remove(category);
            return true;
        });
    }

    public async Task<Category?> GetCategoryByTitleAsync(string title)
    {
        return await Task.Run(() => CategoryList.FirstOrDefault(c => c.Title == title));
    }

    public async Task<Category?> GetCategoryByCodeAsync(string code)
    {
        return await Task.Run(() => CategoryList.FirstOrDefault(c => c.Code == code));
    }

    public Task<List<Category>> GetCategoryToDelete(DateTime difference)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Category>> GetCategoryToDelete()
    {
        return await Task.Run(() => CategoryList.Where(c => c.DeletedAt != null).ToList());
    }

    public Task<bool> DeleteHard(Category category)
    {
        throw new NotImplementedException();
    }
    
}