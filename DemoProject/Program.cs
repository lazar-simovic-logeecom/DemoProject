using DemoProject.Mappings;
using DemoProject.Application;
using FluentValidation.AspNetCore;
using DemoProject.Validators;
using DemoProject.Data;
using DemoProject.Data.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddFluentValidation(cfg =>
    {
        cfg.RegisterValidatorsFromAssemblyContaining<CreateCategoryValidator>();
    });

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();

builder.Services.AddSingleton<CategoryService>(); 

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


