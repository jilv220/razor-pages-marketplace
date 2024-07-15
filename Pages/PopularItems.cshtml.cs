using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PopularItemsModel : PageModel
{
    private readonly PopularItemsService _popularItemsService;

    public List<PopularItems> PopularItems { get; set; }

    public PopularItemsModel(PopularItemsService popularItemsService)
    {
        _popularItemsService = popularItemsService;
    }

    public async Task OnGetAsync()
    {
        PopularItems = await _popularItemsService.GetPopularItemsAsync();
    }
}
