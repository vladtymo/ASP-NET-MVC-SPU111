using AspNet_MVC_SPU111.Models;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNet_MVC_SPU111.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopSPUDbContext ctx;

        public HomeController(ShopSPUDbContext ctx)
        {
            this.ctx = ctx;
        }

        public IActionResult Index()
        {
            // add to the db
            // read
            // modify
            // ...

            return View(ctx.Products.ToList()); // ~/Views/Home/Index.cshtml
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}