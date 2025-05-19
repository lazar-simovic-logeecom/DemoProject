namespace DemoProject.Application.Model;

public class User()
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public string Password { get; set; }
    public string? Role { get; set; } = null;
    public string? Token { get; set; } = null;
}