using DemoProject.Mappings;
using DemoProject.Application;
using DemoProject.Application.Interface;
using FluentValidation.AspNetCore;
using DemoProject.Validators;
using DemoProject.DataEF;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddTransient<ICategoryRepository, CategoryRepositoryEF>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();