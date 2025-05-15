using System.Threading.Channels;
using DemoProject.Mappings;
using DemoProject.Application;
using DemoProject.Application.Interface;
using DemoProject.Application.Services;
using DemoProject.Background;
using FluentValidation.AspNetCore;
using DemoProject.Validators;
using DemoProject.DataEF;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddSingleton(Channel.CreateUnbounded<string>());
builder.Services.AddSingleton<LoggingService>();
builder.Services.AddHostedService<LoggingBackgroundService>();

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddTransient<ICategoryRepository, CategoryRepositoryEf>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();