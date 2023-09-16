using AspNet_MVC_SPU111.Data;
using AspNet_MVC_SPU111.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AspNet_MVC_SPU111.Controllers
{
    public class ProductsController : Controller
    {
        ShopSPUDbContext ctx = new ShopSPUDbContext();

        public IActionResult Index()
        {
            var products = ctx.Products.ToList();

            return View(products);
        }
    }
}
