using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DemoProject.Application.Exceptions;
using DemoProject.Application.Interface;
using DemoProject.Application.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DemoProject.Application.Services;

public class AuthService(IUserRepository userRepository, IConfiguration config) : IAuthService
{
    public async Task<User?> AuthenticateBasicAsync(string username, string password)
    {
        var user = await userRepository.GetByUsernameAsync(username);

        if (user == null)
            return null;

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

        return isPasswordValid ? user : null;
    }

    public async Task<bool> RegisterAsync(User user)
    {
        if (await userRepository.GetUserByUsername(user.Username))
        {
            throw new UserAlreadyExistsException("User with this username already exists.");
        }
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        await userRepository.CreateAsync(user);

        return true;
    }

    public async Task<string?> LoginAsync(string username, string password)
    {
        var user = await userRepository.GetByUsernameAsync(username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Username) }),
            Expires = DateTime.UtcNow.AddMinutes(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        await userRepository.UpdateTokenAsync(user.Id, jwt);
        return jwt;
    }
}