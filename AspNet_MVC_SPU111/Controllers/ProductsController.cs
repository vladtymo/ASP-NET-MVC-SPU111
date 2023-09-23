using AspNet_MVC_SPU111.Data;
using AspNet_MVC_SPU111.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNet_MVC_SPU111.Controllers
{
    public class ProductsController : Controller
    {
        ShopSPUDbContext ctx = new ShopSPUDbContext();

        // Get all products
        public IActionResult Index()
        {
            var products = ctx.Products.ToList();

            return View(products);
        }

        // GET: show create form page
        [HttpGet]
        public IActionResult Create()
        {
            // Ways of sending data to View
            // 1 - use View.Model: return View(obj)
            // 2 - use View.TempData: this.TempDate["Property"] = value
            // 3 - use View.ViewBag: this.ViewBag.Property = value
            this.ViewBag.Categories = new SelectList(ctx.Categories.ToList(), "Id", "Name");

            return View();
        }

        // GET: show create form page
        [HttpPost]
        public IActionResult Create(Product product)
        {
            ctx.Products.Add(product);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        // Delete product by ID
        public IActionResult Delete(int id)
        {
            var item = ctx.Products.Find(id);

            if (item == null) return NotFound();

            ctx.Products.Remove(item);
            ctx.SaveChanges(); // delete from db

            return RedirectToAction("Index");
        }
    }
}
