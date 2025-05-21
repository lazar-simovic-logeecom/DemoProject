using DemoProject.Application.Interface;
using DemoProject.Application.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DataEF;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(c => c.Username == username);
    }

    public async Task CreateAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetByTokenAsync(string token)
    {
        return await context.Users.FirstOrDefaultAsync(c => c.Token == token);
    }

    public async Task UpdateTokenAsync(Guid userId, string token)
    {
        User? user = await context.Users.FindAsync(userId);
        if (user == null)
        {
            return;
        }

        user.Token = token;
        await context.SaveChangesAsync();
    }

    public Task<bool> GetUserByUsername(string username)
    {
        return context.Users.AnyAsync(c => c.Username == username);
    }
}