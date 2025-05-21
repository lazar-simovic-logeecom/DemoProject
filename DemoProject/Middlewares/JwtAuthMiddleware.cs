using System.IdentityModel.Tokens.Jwt;
using DemoProject.Application.Interface;
using DemoProject.Application.Model;
using Microsoft.IdentityModel.Tokens;

namespace DemoProject.Middlewares;

public class JwtAuthMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, IUserRepository repo, TokenValidationParameters validationParameters)
    {
        if (!RequiresAuthentication(context))
        {
            await next(context);

            return;
        }
        
        string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
        if (token == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Missing Authorization header");

            return;
        }

        User? user = await repo.GetByTokenAsync(token);
        if (user == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid token");

            return;
        }

        string method = context.Request.Method;

        if (method != "GET" && user.Role != "Admin")
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Forbidden: User is not an Admin");
            
            return;
        }

        JwtSecurityTokenHandler tokenHandler = new();
        
        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            context.User = principal;
        }
        catch (SecurityTokenExpiredException)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token expired. Please login again.");

            return;
        }
        
        await next(context);
    }

    private bool RequiresAuthentication(HttpContext context)
    {
        var path = context.Request.Path.ToString().ToLower();

        if (path.Contains("/api/login") || path.Contains("/api/register"))
        {
            return false;
        }

        return true;
    }
}