using System;
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
using Granite_House.Extension;

namespace Granite_House.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HostingEnvironment _hostingEnvironment;

        [BindProperty]
        public ProductsViewModels _ProductsVM { get; set; }
        public ProductsController(ApplicationDbContext dbContext, HostingEnvironment hostingEnvironment)
        {
            this._db = dbContext;
            this._hostingEnvironment = hostingEnvironment;

            this._ProductsVM = new ProductsViewModels()
            {
                Products = new Models.Products(),
                ProductTypes = this._db.ProductTypes.ToList(),
                SpecialTags = this._db.SpecialTags.ToList(),
                selectLists = this._db.ProductTypes.ToSelectListItem(),
                specialTagList = this._db.SpecialTags.TogetSelectListOnIdAndData("TagId", "TagName")
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

            if (file.Count > 0)
            {
                //file has been uploaded
                var uploadsFilePath = Path.Combine(webRootPath, Utility.PathToImageFolder.ImageFolder);
                var extensions = Path.GetExtension(file[0].FileName);
                var filenameWithExtension = _ProductsVM.Products.ProductId.ToString() + extensions;

                using (FileStream fileStream = new FileStream(Path.Combine(uploadsFilePath, filenameWithExtension
                                   ), mode: FileMode.CreateNew))
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
        [HttpGet, ActionName("TestDropDown")]
        public string TestDropDown()
        {
            var pvm = this._ProductsVM;
            //todo
            var ProtudctTypes = this._db.ProductTypes.Where(b => b.Name.Contains("this")).FirstOrDefault();
            var val = ProtudctTypes.GetPropertyValue("Name");
            var EnumerableMethodtest = pvm.ProductTypes.ToSelectListItem();


            return val;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            this._ProductsVM.Products = await _db.Products.Include(s => s.ProductTypes).Include(p => p.SpecialTags).Where(m => m.ProductId == id).SingleOrDefaultAsync();
            if (this._ProductsVM.Products == null)
            {
                return NotFound();
            }
            return View(_ProductsVM);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id) //actually etar dorkar chilo na karon 
                                                           //eta post request
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(this._ProductsVM);
            }
            //todo
            //get the file requested from the user
            //check if picture has been added or not 
            //if pic is added , modify the current picture 
            //else put the old picture 
            //default picture 

            //var path
            var webrootpath = this._hostingEnvironment.WebRootPath;
            //files from request
            var files = HttpContext.Request.Form.Files;

            var productsFromDb = await this._db.Products.FindAsync(_ProductsVM.Products.ProductId);

            if (files.Count > 0 && files[0].Length > 0 && files[0] != null) //taile confirm file upload hoise 
            {//file has been uploaded 
                var oldExtension = Path.GetExtension(productsFromDb.Image);
                var newExtension = Path.GetExtension(files[0].FileName);
                var pathTobeUploaded = Path.Combine(webrootpath, Utility.PathToImageFolder.ImageFolder);

                //if the file older file exists
                if (System.IO.File.Exists(Path.Combine(pathTobeUploaded, productsFromDb.ProductId.ToString() + oldExtension)))
                {
                    //delete the old file
                    System.IO.File.Delete(Path.Combine(pathTobeUploaded, productsFromDb.ProductId.ToString() + oldExtension));
                }
                //copy the new file to that place 
                using (var fileStrm = new FileStream(Path.Combine(pathTobeUploaded, productsFromDb.ProductId.ToString() + newExtension), FileMode.Create))
                {
                    files[0].CopyTo(fileStrm);
                    //oi place e file ta copy kora holo 
                }
                //database e update korte hbe 
                _ProductsVM.Products.Image = @"\" + Utility.PathToImageFolder.ImageFolder + @"\" + productsFromDb.ProductId.ToString() + newExtension;


            }//product has been uploaded in both the hard disk and database 

            if (_ProductsVM.Products.Image != null)
            {
                productsFromDb.Image = _ProductsVM.Products.Image;
                //property binding ew update kora hoilo 
            }

            productsFromDb.Price = _ProductsVM.Products.Price;
            productsFromDb.ProductName = _ProductsVM.Products.ProductName;
            productsFromDb.ShadeColor = _ProductsVM.Products.ShadeColor;
            productsFromDb.SpecialTagsId = _ProductsVM.Products.SpecialTagsId;
            productsFromDb.ProductTypesId = _ProductsVM.Products.ProductTypesId;
            productsFromDb.Available = _ProductsVM.Products.Available;
            await this._db.SaveChangesAsync();

            return RedirectToAction(nameof(this.Index));


        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this._ProductsVM.Products = await this._db.Products.Include(m => m.SpecialTags).Include(m => m.ProductTypes)
                .SingleOrDefaultAsync(m => m.ProductId == id);
            if (this._ProductsVM.Products == null)
            {
                return NotFound();
            }
            return View(_ProductsVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            this._ProductsVM.Products = await this._db.Products.Include(m => m.SpecialTags).Include(m => m.ProductTypes)
                .SingleOrDefaultAsync(m => m.ProductId == id);
            if (this._ProductsVM.Products == null)
            {
                return NotFound();
            }
            return View(_ProductsVM);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePermanently(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await this._db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var wwwroot = this._hostingEnvironment.WebRootPath;
            var filepath = System.IO.Path.Combine(wwwroot, Utility.PathToImageFolder.ImageFolder, product.ProductId.ToString()+Path.GetExtension(product.Image ));
            if (System.IO.File.Exists(filepath))
                System.IO.File.Delete(filepath);

            this._db.Products.Remove(product);
            await this._db.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); 

        }




    }
}
