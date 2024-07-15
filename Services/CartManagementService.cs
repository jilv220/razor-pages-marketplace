using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Project.Data;
using Project.Models;
using Project.Services;
using System.Threading.Tasks;

public class CartManagementService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly NavigationManager _navigation;
    private readonly CartService _cartService;
    private readonly IJSRuntime _jsRuntime;

    public CartManagementService(
        ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager,
        AuthenticationStateProvider authenticationStateProvider,
        NavigationManager navigation,
        CartService cartService,
        IJSRuntime jsRuntime)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _authenticationStateProvider = authenticationStateProvider;
        _navigation = navigation;
        _cartService = cartService;
        _jsRuntime = jsRuntime;
    }

    public async Task<bool> IsUserSignedInAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user != null && user.Identity != null && user.Identity.Name != null;
    }

    public async Task AddToCartAsync(string userName, int productId)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            _navigation.NavigateTo("/Identity/Account/Login");
            return;
        }

        await _cartService.AddToCartAsync(userName, productId);
        await ShowAddToCartToastAsync();
    }

    public async Task ShowAddToCartToastAsync()
    {
        await _jsRuntime.InvokeVoidAsync("showAddToCartToast", null);
    }

    public void NavigateToLogin()
    {
        _navigation.NavigateTo("/Identity/Account/Login");
    }

}
