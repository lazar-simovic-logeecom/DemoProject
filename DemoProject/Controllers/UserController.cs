using AutoMapper;
using DemoProject.Application.Exceptions;
using DemoProject.Application.Interface;
using DemoProject.Application.Model;
using DemoProject.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers;

[ApiController]
[Route("api/")]
public class UserController(IAuthService authService, IMapper mapper) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
    {
        try
        {
            User user = mapper.Map<User>(userDto);
            bool isCreated = await authService.RegisterAsync(user);
            if (!isCreated)
            {
                return BadRequest(new { message = "Failed to register user" });
            }

            return Ok("User created");
        }
        catch (UserAlreadyExistsException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var token = await authService.LoginAsync(dto.Username, dto.Password);
        if (token == null)
        {
            return Unauthorized("Invalid credentials");
        }
        return Ok(new { token });
    }
}