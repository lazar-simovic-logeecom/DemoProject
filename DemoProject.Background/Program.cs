using System.Threading.Channels;
using DemoProject.Application.Interface;
using DemoProject.Application.Services;
using DemoProject.Background;
using DemoProject.DataEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
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
    });

var app = builder.Build();

await app.RunAsync();