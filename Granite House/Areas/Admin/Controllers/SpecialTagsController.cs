using Microsoft.AspNetCore.Mvc;
using Granite_House.Data;
using Granite_House.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Granite_House.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagsController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public SpecialTagsController(ApplicationDbContext DbContext)
        {
            this.applicationDbContext = DbContext;
        }

        public IActionResult Index()
        {
            var allTags = applicationDbContext.SpecialTags.ToList();
            return View(allTags);
        }
        //this is get method 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //this is post method 
        [HttpPost]
        [ValidateAntiForgeryToken] //to validate the antiforgery 
        public async Task<IActionResult> Create(SpecialTags tags)
        {
            if (ModelState.IsValid)
            {
                this.applicationDbContext.Add(tags);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(tags); //validation fails 
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var SpecialTags = await this.applicationDbContext.SpecialTags.FindAsync(id);
            if (SpecialTags == null)
            {
                return NotFound();
            }
            return View(SpecialTags);
        }
    }

   this.

}
