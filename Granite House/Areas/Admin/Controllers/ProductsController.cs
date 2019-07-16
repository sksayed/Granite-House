﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Granite_House.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Granite_House.Models.ViewModel;
using Microsoft.AspNetCore.Hosting.Internal;
using System.IO;
using Granite_House.Utility;

namespace Granite_House.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HostingEnvironment _hostingEnvironment;

        [BindProperty]
        public ProductsViewModels _ProductsVM { get; set; }
        public ProductsController(ApplicationDbContext dbContext , HostingEnvironment hostingEnvironment)
        {
            this._db = dbContext;
            this._hostingEnvironment = hostingEnvironment;

            this._ProductsVM = new ProductsViewModels()
            {
                Products = new Models.Products(),
                ProductTypes = this._db.ProductTypes.ToList(),
                SpecialTags = this._db.SpecialTags.ToList()
            };

        }
        public async Task<IActionResult> Index()
        {
            var product = _db.Products.Include(p => p.ProductTypes).Include(p => p.SpecialTags);
            return View(await product.ToListAsync());
        }

        [HttpGet, ActionName("Create")]
        public IActionResult Create()
        {
            return View(this._ProductsVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAProduct()
        {
            //if modelState is not valid one  
            if (!ModelState.IsValid)
            {
                return View(this._ProductsVM);
            }
            //if the model state is valid 
            _db.Products.Add(this._ProductsVM.Products);
            //here we have used bind property
            //it is a technique used in asp.net core
            //to bind the values 
            // otherwise we have to use CreateAProduct( ProductsViewModel porductsViewModel)
            //it uses Attributes to do so 

            //we saved in the database 
            await this._db.SaveChangesAsync();

            //now its time to save an image 

            var webRootPath = this._hostingEnvironment.WebRootPath;
            //this will give us the wwwroot folder path

            var file = HttpContext.Request.Form.Files;
            //in the post method 
            //with the request and from a form 
            //the files that we want to get ;
            //its an Array of files collection

            var productsFromDb = this._db.Products.Find(this._ProductsVM.Products.ProductId);
            //the product which we stored at last 

            if (file.Count >= 0)
            {
                //file has been uploaded
                var uploadsFilePath = Path.Combine(webRootPath, Utility.PathToImageFolder.ImageFolder);
                var extensions = Path.GetExtension(file[0].FileName);

                using (FileStream fileStream = new FileStream(Path.Combine(uploadsFilePath, _ProductsVM.Products.ProductId.ToString(),
                                   extensions), mode: FileMode.CreateNew))
                {
                    file[0].CopyTo(fileStream);

                }

                productsFromDb.Image = @"\" + PathToImageFolder.ImageFolder + @"\" + productsFromDb.ProductId + extensions;

            }
            else
            {

                //when user does not upload image
                var uploadsFilePath = Path.Combine(webRootPath, PathToImageFolder.ImageFolder + @"\" + PathToImageFolder.DefaultImage);
                System.IO.File.Copy(uploadsFilePath, webRootPath + @"\" + PathToImageFolder.ImageFolder + @"\" + _ProductsVM.Products.ProductId.ToString() + ".png");
                productsFromDb.Image = @"\" + PathToImageFolder.ImageFolder + @"\" + _ProductsVM.Products.ProductId + ".png";
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        
    }
}
