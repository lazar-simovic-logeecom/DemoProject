using DemoProject.Application.Interface;
using DemoProject.Application.Services;
using DemoProject.Background.Services;
using DemoProject.DataEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((config) =>
    {
        config.Sources.Clear();

        config.AddJsonFile("appsettings.background.json", optional: false, reloadOnChange: true);

        config.AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddSingleton<LoggingService>();
        services.AddHostedService<DeleteBackgroundService>();
        services.AddScoped<ICategoryRepository, CategoryRepositoryEf>();
        services.AddScoped<IProductRepository, ProductRepositoryEf>();
        services.AddScoped<IProductService, ProductService>();
        services.AddHostedService<GetByIdBackgroundService>();
    });

var app = builder.Build();

await app.RunAsync();