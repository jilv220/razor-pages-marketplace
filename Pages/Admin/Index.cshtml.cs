using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly Project.Data.ApplicationDbContext _context;

        public IndexModel(Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}
