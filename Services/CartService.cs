using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using System.Threading.Tasks;

namespace Project.Services;

public class CartService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<int> GetCartItemCountAsync(string userId)
    {
        var user = _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return 0;
        }

        var cart = await _dbContext.Carts
            .Include(c => c.CartProducts)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        return cart?.CartProducts.Sum(cp => cp.Quantity) ?? 0;
    }
}
