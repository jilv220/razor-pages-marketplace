using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using System.Threading.Tasks;

namespace Project.Services;

public class CartService
{
    private readonly ApplicationDbContext _dbContext;

    public delegate void CartChangedEventHandler(object sender, EventArgs e);
    public event CartChangedEventHandler CartChanged;

    protected virtual void OnCartChanged()
    {
        CartChanged?.Invoke(this, EventArgs.Empty);
    }

    public CartService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddToCartAsync(string email, int productId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email)
            ?? throw new Exception("User not found");

        var cart = await _dbContext.Carts
            .Include(c => c.CartProducts)
            .FirstOrDefaultAsync(c => c.UserId == user.Id);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = user.Id,
                CartProducts = new List<CartProduct>(),
                User = user
            };

            _dbContext.Carts.Add(cart);
        }

        var cartProduct = cart.CartProducts.FirstOrDefault(cp => cp.ProductId == productId);
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId)
            ?? throw new Exception("Product not found");

        if (cartProduct != null)
        {
            cartProduct.Quantity++;
        }
        else
        {
            cartProduct = new CartProduct
            {
                CartId = cart.Id,
                Cart = cart,
                ProductId = productId,
                Product = product,
                Quantity = 1
            };

            cart.CartProducts.Add(cartProduct);
        }

        await _dbContext.SaveChangesAsync();
        OnCartChanged();
    }

    public async Task<int> GetCartItemCountAsync(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            return 0;
        }

        var cart = await _dbContext.Carts
            .Include(c => c.CartProducts)
            .FirstOrDefaultAsync(c => c.UserId == user.Id);

        return cart?.CartProducts.Sum(cp => cp.Quantity) ?? 0;
    }
}
