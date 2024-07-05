using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckoutModel(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public Cart? Cart { get; set; }
        public decimal TotalAmount { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            Cart = await _dbContext.Carts
                .Include(c => c.CartProducts)
                .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            TotalAmount = Cart?.CartProducts.Sum(cp => cp.Product.UnitPrice * cp.Quantity) ?? 0;
            return Page();
        }

        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var cart = await _dbContext.Carts
                .Include(c => c.CartProducts)
                .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null)
            {
                return RedirectToPage("/Index");
            }

            foreach (var cartProduct in cart.CartProducts)
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == cartProduct.ProductId);
                if (product != null && product.QtyInStock >= cartProduct.Quantity)
                {
                    product.QtyInStock -= cartProduct.Quantity;
                }
            }
            await _dbContext.SaveChangesAsync();

            // Clear the cart after checkout
            _dbContext.CartProducts.RemoveRange(cart.CartProducts);
            _dbContext.Carts.Remove(cart);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
