using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Project.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Project.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public bool ShowSuccessMessage { get; set; }

        [BindProperty]
        public bool ShowErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                ShowErrorMessage = true;
                return Page();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ShowErrorMessage = true;
                return NotFound($"Unable to load user with id: '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            // Console.WriteLine(result.Errors.FirstOrDefault()?.Code);
            // Console.WriteLine(result.Errors.FirstOrDefault()?.Description);

            if (result.Succeeded)
            {
                ShowSuccessMessage = true;
            }
            else
            {
                ShowErrorMessage = true;
            }

            return Page();
        }
    }
}