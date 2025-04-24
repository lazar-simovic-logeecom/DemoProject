using DemoProject.Mappings;
using DemoProject.service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddSingleton<CategoryService>(); 

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


