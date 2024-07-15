using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Services;

namespace Project.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly Project.Data.ApplicationDbContext _context;
        private readonly UploadService _uploadService;

        public CreateModel(Project.Data.ApplicationDbContext context, UploadService uploadService)
        {
            _context = context;
            _uploadService = uploadService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public IFormFile? Upload { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _context.Products.Add(Product);
                await _context.SaveChangesAsync();

                if (Upload != null)
                {
                    Product.ImageUri = await _uploadService.UploadImageAsync(Upload, Product.Id);
                    Product.CreateTime = DateTime.Now;
                    _context.Attach(Product).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
