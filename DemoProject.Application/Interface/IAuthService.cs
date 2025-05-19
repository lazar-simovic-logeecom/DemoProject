using DemoProject.Application.Model;

namespace DemoProject.Application.Interface;

public interface IAuthService
{
    Task<User?> AuthenticateBasicAsync(string username, string password);
    Task<bool> RegisterAsync(User user);
    Task<string?> LoginAsync(string username, string password);
}