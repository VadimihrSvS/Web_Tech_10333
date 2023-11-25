using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AspNet_Projects.Extensions;
using AspNet_Projects.Models;


public class CartViewComponent : ViewComponent
{
    private Cart _cart;

    public CartViewComponent(Cart cart)
    {
        _cart = cart;
    }
    public IViewComponentResult Invoke()
    {
        return View(_cart);
    }
}