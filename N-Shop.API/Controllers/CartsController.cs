using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using N_Shop.API.DTOs.Responses;
using N_Shop.API.Models;
using N_Shop.API.Services;

namespace N_Shop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly UserManager<ApplicationUser> _userManager;
    public CartsController(ICartService cartService,UserManager<ApplicationUser> userManager)
    {
        _cartService = cartService;
        _userManager = userManager;
    }

    [HttpPost("{ProductId}")]
    public async Task<IActionResult> AddToCart([FromRoute]int ProductId,[FromQuery]int count = 1)
    {
        var appUser = _userManager.GetUserId(User);
        var cart = new Cart{ApplicationUserId = appUser,ProductId = ProductId,Count = count};
        await _cartService.AddAsync(cart);
        return Ok(cart.Adapt<CartResponse>());
    }
}