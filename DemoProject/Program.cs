using DemoProject.Mappings;
using DemoProject.Application;
using FluentValidation.AspNetCore;
using DemoProject.Validators;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddFluentValidation(cfg =>
    {
        cfg.RegisterValidatorsFromAssemblyContaining<CreateCategoryValidator>();
    });

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddSingleton<CategoryService>(); 

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


