using Ecommerce_Project.Extensions;
using Ecommerce_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_Project.Extensions;
using Ecommerce_Project.ViewModels;


namespace Ecommerce_Project.ViewComponents
{
  public class CartCountViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke()
    {
      var cart = HttpContext.Session.GetObject<CartViewModel>("ShoppingCart");
      var count = cart?.ItemCount ?? 0;
      return View(count);
    }
  }
}
