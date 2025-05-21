using DemoProject.Application.Interface;
using DemoProject.Application.Model;
using Microsoft.Extensions.Hosting;

namespace DemoProject.Background.Services;

public class GetByIdBackgroundService(IProductService productService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await RemoveFeatured();
            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
        }
    }

    private async Task RemoveFeatured()
    {
        DateTime time = DateTime.UtcNow.AddSeconds(-20);
        List<Product> featured = await productService.GetProductToRemoveAsync(time);

        foreach (var p in featured)
        {
            await productService.RemoveFeatured(p);
        }
    }
}