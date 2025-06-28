using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BraveHeartBackend.Models;
using BraveHeartBackend.Services;
using BraveHeartBackend.DTOs.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;
    private readonly JwtService _jwtService;

    public UserController(
        UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager, 
        IConfiguration config,
        JwtService jwtService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
        _jwtService = jwtService;
    }

    // 🔓 Register a Customer (public)
    [HttpPost("register-customer")]
    public async Task<IActionResult> RegisterCustomer(RegisterDTO dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        if (!await _roleManager.RoleExistsAsync("Customer"))
            await _roleManager.CreateAsync(new IdentityRole("Customer"));

        await _userManager.AddToRoleAsync(user, "Customer");
        return Ok("Customer registered successfully.");
    }

    // Only Admin can register a BusinessOwner
    [HttpPost("register-business-owner")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterBusinessOwner(RegisterDTO dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        if (!await _roleManager.RoleExistsAsync("BusinessOwner"))
            await _roleManager.CreateAsync(new IdentityRole("BusinessOwner"));

        await _userManager.AddToRoleAsync(user, "BusinessOwner");
        return Ok("Business owner registered successfully.");
    }

    // Login with refresh token support
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            return Unauthorized("Invalid credentials.");

        // Generate tokens using the JWT service
        var tokenResponse = await _jwtService.GenerateTokensAsync(user);
        
        return Ok(tokenResponse);
    }

    // Refresh token endpoint
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenDTO dto)
    {
        var tokenResponse = await _jwtService.RefreshTokenAsync(dto.AccessToken, dto.RefreshToken);
        
        if (tokenResponse == null)
            return Unauthorized("Invalid refresh token.");

        return Ok(tokenResponse);
    }

    // Revoke refresh token (logout)
    [HttpPost("revoke-token")]
    [Authorize]
    public async Task<IActionResult> RevokeToken()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User ID not found in token.");

        var success = await _jwtService.RevokeRefreshTokenAsync(userId);
        if (!success)
            return BadRequest("Failed to revoke token.");

        return Ok("Token revoked successfully.");
    }

    // 🔐 Admin-only: List all users
    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAllUsers()
    {
        var users = _userManager.Users.Select(u => new
        {
            u.Id,
            u.Email
        }).ToList();

        return Ok(users);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized("User ID not found in token.");

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return NotFound("User not found.");

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new
        {
            user.Id,
            user.Email,
            Roles = roles
        });
    }
}