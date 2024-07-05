using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Services;

namespace Project.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        private readonly UploadService _uploadService;

        public EditModel(
            Project.Data.ApplicationDbContext context, IWebHostEnvironment environment, UploadService uploadService)
        {
            _context = context;
            _environment = environment;
            _uploadService = uploadService;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public IFormFile? Upload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Product.ImageUri");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == Product.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            try
            {
                if (Upload != null)
                {
                    Product.ImageUri = await _uploadService.UploadImageAsync(Upload, Product.Id);
                }
                else
                {
                    Product.ImageUri = existingProduct.ImageUri;
                }

                _context.Attach(Product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
