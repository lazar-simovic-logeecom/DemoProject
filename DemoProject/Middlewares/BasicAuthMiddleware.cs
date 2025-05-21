using System.Text;
using DemoProject.Application.Interface;
using DemoProject.Application.Model;

namespace DemoProject.Middlewares;

public class BasicAuthMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IAuthService authService)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Missing Authorization header");

            return;
        }

        if (!authHeader.ToString().StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("This is not basic auth");

            return;
        }

        try
        {
            string encoded = authHeader.ToString()["Basic".Length..].Trim();
            string decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
            string[] credentials = decoded.Split(':');

            if (credentials.Length != 2)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Authorization header");
                
                return;
            }

            string username = credentials[0];
            string password = credentials[1];

            User? user = await authService.AuthenticateBasicAsync(username, password);

            if (user == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid username or password");

                return;
            }

            string method = context.Request.Method;

            if (method != "GET" && user.Role != "Admin")
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Forbidden: User is not an Admin");
                
                return;
            }
            
            context.Items["User"] = user;
            await next(context);
        }
        catch
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Authentication failed.");
        }
    }
}