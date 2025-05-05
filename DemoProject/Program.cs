using DemoProject.Mappings;
using DemoProject.Application;
using DemoProject.Application.Interface;
using FluentValidation.AspNetCore;
using DemoProject.Validators;
using DemoProject.Data;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();