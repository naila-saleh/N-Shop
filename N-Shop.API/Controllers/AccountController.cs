using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using N_Shop.API.DTOs.Requests;
using N_Shop.API.Models;

namespace N_Shop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        var applicationUser= registerRequest.Adapt<ApplicationUser>();
        var result = await _userManager.CreateAsync(applicationUser, registerRequest.Password);
        if (result.Succeeded){
            await _signInManager.SignInAsync(applicationUser, false);
            return NoContent();
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var applicationUser = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (applicationUser != null)
        {
            var result = await _userManager.CheckPasswordAsync(applicationUser, loginRequest.Password);
            if (result)
            {
                await _signInManager.SignInAsync(applicationUser, loginRequest.RememberMe);
                return NoContent();
            }
        }

        return BadRequest(new { message = "Email or password is incorrect" });
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return NoContent();
    }

    [HttpPost("ChangePassword")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
    {
        var applicationUser = await _userManager.GetUserAsync(User);
        if (applicationUser != null)
        {
            var result = await _userManager.ChangePasswordAsync(applicationUser, changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);
            if (result.Succeeded) return NoContent();
            else return BadRequest(result.Errors);
        }
        return BadRequest(new { message = "Invalid data" });
    }
}