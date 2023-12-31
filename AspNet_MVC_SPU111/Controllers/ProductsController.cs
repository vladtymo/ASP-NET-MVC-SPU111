﻿using AspNet_MVC_SPU111.Helpers;
using AspNet_MVC_SPU111.Models;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNet_MVC_SPU111.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly ShopSPUDbContext ctx;
        private readonly IFileService fileService;

        public ProductsController(ShopSPUDbContext ctx, IFileService fileService)
        {
            this.ctx = ctx;
            this.fileService = fileService;
        }

        private void LoadCategories()
        {
            this.ViewBag.Categories = new SelectList(ctx.Categories.ToList(), "Id", "Name");
        }

        // Get all products
        [AllowAnonymous]
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
            //this.ViewBag.Categories = new SelectList(ctx.Categories.ToList(), "Id", "Name");
            LoadCategories();

            return View();
        }

        // POST: add product to the database
        [HttpPost]
        public async Task <IActionResult> Create(CreateProductModel model)
        {
            // validate all Product properties
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View(model);
            }

            var product = new Product()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Discount = model.Discount,
                InStock = model.InStock,
                Price = model.Price,
                ImageUrl = await fileService.SaveProductImage(model.ImageFile) // save image to the server
            };

            ctx.Products.Add(product);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var item = ctx.Products.Find(id);

            if (item == null) return NotFound();

            LoadCategories();

            return View(item);
        }

        // POST: update product in the database
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            // validate all Product properties
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View(product);
            }

            ctx.Products.Update(product);
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
