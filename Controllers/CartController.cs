using Microsoft.AspNetCore.Mvc;
using Lab_WT_Data.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using AspNet_Projects.Extensions;
using AspNet_Projects.Models;

namespace AspNet_Projects.Controllers;
public class CartController : Controller
{
    private ApplicationDbContext _context;
    private string cartKey = "cart";
    private Cart _cart;

    public CartController(ApplicationDbContext context, Cart cart)
    {
        _context = context;
        _cart = cart;
    }
    public IActionResult Index()
    {
        _cart = HttpContext.Session.Get<Cart>(cartKey);
        return View(_cart.Items.Values);
    }
    [Authorize]
    public IActionResult Add(int id, string returnUrl)
    {
        _cart = HttpContext.Session.Get<Cart>(cartKey);
        var item = _context.Dishes.Find(id);
        if (item != null)
        {
            _cart.AddToCart(item);
            HttpContext.Session.Set<Cart>(cartKey, _cart);
        }
        return Redirect(returnUrl);
    }
}
