using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Products;

            foreach (var obj in objList)
            {
                obj.Category = _db.Categories.FirstOrDefault(u => u.Id == obj.CategoryId);
            }

            return View(objList);
        }

        //Get for upsert
        public IActionResult Upsert(int? id)
        {
            Product product = new Product();

            if (id == null)
            {
                //this is for create
                return View(product);
            }

            else
            {
                product = _db.Products.Find(id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }

        }

        //Post for upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertPost(Product obj)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //Get for Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product obj = _db.Products.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //Post for Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            Product obj = _db.Products.Find(id);

            if (obj == null)
            {
                return View(obj);
            }

            _db.Products.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}

