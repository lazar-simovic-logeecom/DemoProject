using DemoProject.Mappings;
using DemoProject.Application;
using DemoProject.Application.Interface;
using FluentValidation.AspNetCore;
using DemoProject.Validators;
using DemoProject.Application.Model;
using DemoProject.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<CreateCategoryValidator>(); });

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<CategoryService>();

/*builder.Services.Scan(scan => scan
    .FromAssemblyOf<ICategoryRepository>()
    .AddClasses(classes => classes.AssignableTo<ICategoryRepository>())
    .AsImplementedInterfaces()
    .WithSingletonLifetime()
);

builder.Services.Scan(scan => scan
    .FromAssemblyOf<ICategoryService>()
    .AddClasses(classes => classes.AssignableTo<ICategoryService>())
    .AsImplementedInterfaces()
    .WithSingletonLifetime()
);*/


var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();