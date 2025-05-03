using Microsoft.AspNetCore.Identity;
using N_Shop.API.Data;
using N_Shop.API.Models;
using N_Shop.API.Services.IService;

namespace N_Shop.API.Services;

public class UserService:Service<ApplicationUser>,IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public UserService(ApplicationDbContext context,UserManager<ApplicationUser> userManager) : base(context)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<bool> ChangeRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            //remove old roles
            var oldRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user,oldRoles);
            //add new role
            var result = await _userManager.AddToRoleAsync(user,roleName);
            if(result.Succeeded) return true;
        }
        return false;
    }
}