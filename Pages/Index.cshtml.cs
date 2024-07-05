using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using Project.Data;
using Project.Models;

namespace Project.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _logger = logger;
        _userManager = userManager;
    }

    public IList<Product> Products { get; set; }

    [BindProperty]
    public Cart? Cart { get; set; }

    public async Task OnGetAsync()
    {
        Products = await _dbContext.Products.ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync(int productId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        Cart = await _dbContext.Carts
            .Include(c => c.CartProducts)
            .FirstOrDefaultAsync(c => c.UserId == user.Id);

        if (Cart == null)
        {
            Cart = new Cart
            {
                UserId = user.Id,
                CartProducts = new List<CartProduct>(),
                User = user
            };

            _dbContext.Carts.Add(Cart);
        }

        var cartProduct = Cart.CartProducts.FirstOrDefault(cp => cp.ProductId == productId);

        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (product == null)
        {
            return NotFound();
        }

        if (cartProduct != null)
        {
            cartProduct.Quantity++;
        }
        else
        {
            cartProduct = new CartProduct
            {
                CartId = Cart.Id,
                Cart = Cart,
                ProductId = productId,
                Product = product,
                Quantity = 1
            };

            Cart.CartProducts.Add(cartProduct);
        }

        await _dbContext.SaveChangesAsync();
        return RedirectToPage();
    }
}


