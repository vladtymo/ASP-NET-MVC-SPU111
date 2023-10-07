using AspNet_MVC_SPU111.Helpers;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspNet_MVC_SPU111.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ShopSPUDbContext ctx;

        private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public OrdersController(ShopSPUDbContext ctx)
        {
            this.ctx = ctx;
        }

        public IActionResult Index()
        {
            var items = ctx.Orders.Where(x => x.UserId == CurrentUserId).ToList();

            return View(items);
        }

        public IActionResult Create()
        {
            List<int>? ids = HttpContext.Session.Get<List<int>>("cart_items");

            List<Product> products = new();

            if (ids != null)
                products = ctx.Products.Where(x => ids.Contains(x.Id)).ToList();

            var order = new Order()
            {
                Date = DateTime.Now,
                UserId = CurrentUserId,
                Products = products,
                TotalPrice = products.Sum(x => x.Price)
            };

            ctx.Orders.Add(order);
            ctx.SaveChanges();
            
            // clear cart items
            HttpContext.Session.Remove("cart_items");

            return RedirectToAction("Index");
        }
    }
}
