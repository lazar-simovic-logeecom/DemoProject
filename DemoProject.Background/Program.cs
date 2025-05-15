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
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddSingleton(Channel.CreateUnbounded<string>());
        services.AddSingleton<LoggingService>();
        services.AddHostedService<LoggingBackgroundService>();
        services.AddHostedService<DeleteBackgroundService>();
        services.AddScoped<ICategoryRepository, CategoryRepositoryEf>();
    });

var app = builder.Build();

await app.RunAsync();