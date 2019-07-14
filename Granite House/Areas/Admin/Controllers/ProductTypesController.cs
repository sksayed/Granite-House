using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Granite_House.Data;
using Granite_House.Models;
namespace Granite_House.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());
        }

        //get request
        public IActionResult Create()
        {
            return View();

        }

        //here is the post request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                this._db.Add(productTypes);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(productTypes);
            }


        }
        //Get method for Edit 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductTypes product = await _db.ProductTypes.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //post method for edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductTypes productTypes)
        {
            if (id != productTypes.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);

        }
        //get method for details

        public async Task<IActionResult> Details(int? id)
        {
            //id na pawa gele not found
            if (id == null)
            {
                return NotFound();
            }
            var product = await _db.ProductTypes.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);


        }

        //Get method for Delete 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductTypes product = await _db.ProductTypes.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //post method for edit
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var products = await _db.ProductTypes.FindAsync(id);
            _db.Remove(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           

        }


    }
}