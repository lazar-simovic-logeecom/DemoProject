using DemoProject.Application;
using DemoProject.Application.Interface;
using DemoProject.Application.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DemoProject.Background;

public class DeleteBackgroundService(IServiceScopeFactory scope) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using IServiceScope scopeFactory = scope.CreateScope();
            ICategoryRepository categoryRepository = scopeFactory.ServiceProvider.GetRequiredService<ICategoryRepository>();
            List<Category> toDelete = await categoryRepository.GetCategoryToDelete();

            Console.Write(toDelete.Count);
            
            foreach (Category category in toDelete)
            {
                await categoryRepository.DeleteHard(category);
            }
            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
        }
    }
}