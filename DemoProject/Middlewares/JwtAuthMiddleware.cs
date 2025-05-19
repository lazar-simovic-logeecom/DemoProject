using System.IdentityModel.Tokens.Jwt;
using DemoProject.Application.Interface;
using Microsoft.IdentityModel.Tokens;

namespace DemoProject.Middlewares;

public class JwtAuthMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, IUserRepository repo, TokenValidationParameters validationParameters)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!RequiresAuthentication(context))
        {
            await next(context);

            return;
        }

        if (token == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Missing Authorization header");

            return;
        }

        var user = await repo.GetByTokenAsync(token);
        if (user == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid token");

            return;
        }

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            context.User = principal;
        }
        catch (SecurityTokenExpiredException)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token expired. Please login again.");

            return;
        }
        catch (Exception)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid token");

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

        if (context.Request.Method != HttpMethods.Post)
        {
            return false;
        }

        return true;
    }
}