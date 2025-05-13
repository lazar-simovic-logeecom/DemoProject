using DemoProject.Application.Interface;
using DemoProject.Application.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DataEF;

public class CategoryRepositoryEf(AppDbContext context) : ICategoryRepository
{
    public async Task AddCategoryAsync(Category category)
    {
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid? id)
    {
        return await context.Categories.Include(c => c.SubCategories).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task<Category?> UpdateAsync(Category category)
    {
        await context.SaveChangesAsync();

        return category;
    }

    public async Task<bool> DeleteAsync(Category category)
    {
        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<Category?> GetCategoryByTitleAsync(string title)
    {
        return await context.Categories.FirstOrDefaultAsync(x => x.Title == title);
    }

    public async Task<Category?> GetCategoryByCodeAsync(string code)
    {
        return await context.Categories.FirstOrDefaultAsync(x => x.Code == code);
    }
}