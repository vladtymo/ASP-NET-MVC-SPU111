using AspNet_MVC_SPU111.Helpers;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AspNet_MVC_SPU111.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopSPUDbContext ctx;

        public CartController(ShopSPUDbContext ctx)
        {
            this.ctx = ctx;
        }
        public IActionResult Index()
        {
            List<int>? ids = HttpContext.Session.Get<List<int>>("cart_items");

            List<Product> products = new();

            if (ids != null)
                products = ctx.Products.Where(x => ids.Contains(x.Id)).ToList();

            return View(products);
        }

        public IActionResult Add(int id)
        {
            List<int>? ids = HttpContext.Session.Get<List<int>>("cart_items");

            if (ids == null)
                ids = new List<int>();

            ids.Add(id);

            HttpContext.Session.Set("cart_items", ids);

            return RedirectToAction("Index", "Home");
        }
    }
}
