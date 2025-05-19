using DemoProject.Application.Model;

namespace DemoProject.Application.Interface;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByTokenAsync(string token);
    Task CreateAsync(User user);
    Task UpdateTokenAsync(Guid userId, string token);
    Task<bool> GetUserByUsername(string userUsername);
}