using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProductsCategories.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProductsCategories.Controllers
{
    public class HomeController : Controller
    {

        private YourContext dbContext;
        public HomeController (YourContext context) {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.Products = dbContext.Products.ToList();
            return View();
        }

        [HttpPost("newProduct")]
        public IActionResult newProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                Product addnew = dbContext.Products.SingleOrDefault(p => p.ProductName == product.ProductName);
                if (addnew != null)
                {
                    ViewBag.Message = "This product already exists in the database. Add something else!";
                    return View("Index", product);
                }

                Product NewProduct = new Product 
                {
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductPrice = product.ProductPrice,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                dbContext.Add(NewProduct);
                dbContext.SaveChanges();
                NewProduct = dbContext.Products.SingleOrDefault(p => p.ProductName == NewProduct.ProductName);
                HttpContext.Session.SetInt32("ProductId", NewProduct.ProductId);

                return RedirectToAction("Index");
                
            }
            else {
                return View("Index", product);
            }
        }

        [HttpGet("Category")]
        public IActionResult Category()
        {
            ViewBag.Categories = dbContext.Categories.ToList();
            return View("Category");
        }

        [HttpPost("Category")]
        public IActionResult newCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                // Category addcat = _context.Categories.SingleOrDefault(category => category.Name == addcategory.Name);
                // if (addcat != null)
                // {
                //     ViewBag.Message = "This category already exists in the database. Add something else!";
                //     return View("category", addcategory);
                // }
                Category NewCategory = new Category
                {
                    CategoryName = category.CategoryName,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                dbContext.Add(NewCategory);
                dbContext.SaveChanges();
                NewCategory = dbContext.Categories.SingleOrDefault(c => c.CategoryName == category.CategoryName);
                HttpContext.Session.SetInt32("CategoryId", NewCategory.CategoryId);
                return RedirectToAction("Category");
            }
            else {
                return View("Category", category);
            }

        }

        [HttpGet("~/product/{ProductId}")]
        public IActionResult ShowProduct(int ProductId)
        {
            List<Category> Categories = dbContext.Categories.ToList();
            foreach (var item in Categories)
            {
                System.Console.WriteLine(!item.CategoryName.Contains("Kitchen"));
            }

            Product prodinfo = dbContext.Products.Include(p => p.Association).ThenInclude(a => a.Category).SingleOrDefault(p => p.ProductId == ProductId);

            List<Category> includedCategories = prodinfo.Association.Select(i => i.Category).ToList();
            ViewBag.includedCategories=includedCategories;

            List<Category> otherCategories = Categories.Except(includedCategories).ToList();
            ViewBag.newCats=otherCategories;
            
        //List<Category> prodCategories = dbContext.Categories.Select(p => p.Association.CategoryId == ProductId);
            foreach (var item in prodinfo.Association)
            {
                System.Console.WriteLine(item.Category.CategoryName);
            }
            ViewBag.prodinfo = prodinfo;
            ViewBag.Categories = Categories;
            return View();
        }

        [HttpGet("~/category/{CategoryId}")]
        public IActionResult ShowCategory(int CategoryId)
        {
            List<Product> Products = dbContext.Products.ToList();

            Category catinfo = dbContext.Categories.Include(p => p.Association).ThenInclude(a => a.Product).SingleOrDefault(a => a.CategoryId == CategoryId);
            ViewBag.catinfo = catinfo;
            ViewBag.Products = Products; 
            return View();
        }

        [HttpPost("AddProductToCategory")]
        public IActionResult AddProductToCategory(Association association)
        {
            Association ProdToCat = new Association
            {
                CategoryId = association.CategoryId,
                ProductId = association.ProductId
            };

            dbContext.Add(ProdToCat);
            dbContext.SaveChanges();
            return RedirectToAction("ShowCategory", new { CategoryId = association.CategoryId});
        }

        [HttpPost("AddCatToProd")]
        public IActionResult AddCatToProd(Association association)
        {
            Association CatToProd = new Association
            {
                CategoryId = association.CategoryId,
                ProductId = association.ProductId
            };

            dbContext.Add(CatToProd);
            dbContext.SaveChanges();
            return RedirectToAction("ShowProduct", new { ProductId = association.ProductId});
        }


    }
}
