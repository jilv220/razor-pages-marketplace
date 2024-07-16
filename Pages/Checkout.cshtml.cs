using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IActionResult> OnPostAsync(int[] Quantities, int[] ProductIds)
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

            // 更新购物车数量
            for (int i = 0; i < ProductIds.Length; i++)
            {
                var cartProduct = cart.CartProducts.FirstOrDefault(cp => cp.ProductId == ProductIds[i]);
                if (cartProduct != null)
                {
                    cartProduct.Quantity = Quantities[i];
                }
            }

            // 创建订单
            var order = new Orders
            {
                UserId = user.Id,
                OrderDate = DateTime.Now
            };
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            // 创建订单详细信息
            foreach (var cartProduct in cart.CartProducts)
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == cartProduct.ProductId);
                if (product != null && product.QtyInStock >= cartProduct.Quantity)
                {
                    product.QtyInStock -= cartProduct.Quantity;

                    var orderDetail = new OrderDetails
                    {
                        OrderId = order.Id,
                        ProductId = cartProduct.ProductId,
                        Quantity = cartProduct.Quantity,
                        TotalPrice = product.UnitPrice * cartProduct.Quantity
                    };
                    _dbContext.OrderDetails.Add(orderDetail);
                }
            }
            await _dbContext.SaveChangesAsync();

            // 清空购物车
            _dbContext.CartProducts.RemoveRange(cart.CartProducts);
            _dbContext.Carts.Remove(cart);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
