using DemoProject.service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<CategoryService>(); 

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


