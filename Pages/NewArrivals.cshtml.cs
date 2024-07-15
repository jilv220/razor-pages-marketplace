
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class NewArrivalsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public NewArrivalsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Product> Products { get; set; }

    public async Task OnGetAsync()
    {
        Products = await _context.Products
            .OrderByDescending(p => p.CreateTime)
            .ToListAsync();


    }

}
