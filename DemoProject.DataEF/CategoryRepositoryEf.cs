using DemoProject.Application.Interface;
using DemoProject.Application.Model;
using DemoProject.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DataEF;

public class CategoryRepositoryEf(AppDbContext context, LoggingService loggingService) : ICategoryRepository
{
    public async Task AddCategoryAsync(Category category)
    {
        await loggingService.LogAsync("Adding category");
        context.Categories.Add(category);
        await context.SaveChangesAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid? id)
    {
        await loggingService.LogAsync($"Researching category with id = {id}");
        return await context.Categories.Include(c => c.SubCategories).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Category>> GetAllAsync()
    {
        await loggingService.LogAsync("Getting all categories");
        return await context.Categories.Where(c => c.DeletedAt == null).ToListAsync();
    }

    public async Task<Category?> UpdateAsync(Category category)
    {
        await loggingService.LogAsync($"Updating category with id = {category.Id}");
        await context.SaveChangesAsync();

        return category;
    }

    public async Task<bool> DeleteAsync(Category category)
    {
        await loggingService.LogAsync($"Deleting category with id = {category.Id}");
        category.DeletedAt = DateTime.UtcNow;
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

    public async Task<List<Category>> GetCategoryToDelete(DateTime difference)
    {
        return await context.Categories
            .Where(c => c.DeletedAt != null && c.DeletedAt <= difference).ToListAsync();
    }

    public async Task<bool> DeleteHard(Category category)
    {
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        
        return true;
    }
}