using N_Shop.API.Data;
using N_Shop.API.Models;
using N_Shop.API.Services.IService;

namespace N_Shop.API.Services;

public class CartService:Service<Cart>,ICartService
{
    private readonly ApplicationDbContext _context;
    public CartService(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}