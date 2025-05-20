using System.Text;
using System.Threading.Channels;
using DemoProject.Mappings;
using DemoProject.Application;
using DemoProject.Application.Interface;
using DemoProject.Application.Services;
using DemoProject.Background.Services;
using FluentValidation.AspNetCore;
using DemoProject.Validators;
using DemoProject.DataEF;
using DemoProject.Middlewares;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepositoryEf>();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key)
};

builder.Services.AddSingleton(tokenValidationParameters);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = tokenValidationParameters;
    });


var app = builder.Build();

//app.UseMiddleware<BasicAuthMiddleware>();
app.UseMiddleware<JwtAuthMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();