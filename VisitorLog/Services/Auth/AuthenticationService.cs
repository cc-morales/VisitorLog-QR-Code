using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VisitorLog.DTOs;
using VisitorLog.Models;

namespace VisitorLog.Services.Auth;

public interface IAuthenticationService
{
    Task<AuthResult> LoginAsync(string username, string password);
    Task<AuthResult> CreateAccountAsync(string username, string email, string fullName, string password, bool isAdmin = false);
    Task<List<ApplicationUser>> GetAllUsersAsync();
    Task<bool> DeleteUserAsync(string userId);
    Task<ApplicationUser?> GetUserByIdAsync(string userId);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthenticationService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<AuthResult> LoginAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return new AuthResult { Success = false, Message = "Invalid username or password" };
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
        {
            return new AuthResult { Success = false, Message = "Invalid username or password" };
        }

        var token = await GenerateJwtToken(user);
        return new AuthResult 
        { 
            Success = true, 
            Token = token, 
            UserName = user.UserName!, 
            FullName = user.FullName 
        };
    }

    public async Task<AuthResult> CreateAccountAsync(string username, string email, string fullName, string password, bool isAdmin = false)
    {
        var existingUser = await _userManager.FindByNameAsync(username);
        if (existingUser != null)
        {
            return new AuthResult { Success = false, Message = "Username already exists" };
        }

        var user = new ApplicationUser
        {
            UserName = username,
            Email = email,
            FullName = fullName,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return new AuthResult 
            { 
                Success = false, 
                Message = string.Join(", ", result.Errors.Select(e => e.Description)) 
            };
        }

        await _userManager.AddToRoleAsync(user, isAdmin ? "Admin" : "User");

        return new AuthResult { Success = true, Message = "Account created successfully" };
    }

    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        return _userManager.Users.ToList();
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    private async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim("FullName", user.FullName),
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["Jwt:Key"] ?? "88613hj12beda7T6!@T#!@Y)(!@M)1d,8xyM&@T#UY@G$UIC&*BWQ!"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(1);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"] ?? "VisitorLogIssuer",
            audience: _configuration["Jwt:Audience"] ?? "VisitorLogAudience",
            claims: claims,
            expires: expiration,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}