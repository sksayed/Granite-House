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


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditandSaveChangesToDb(int id, SpecialTags tags)
        {
            if (id != tags.TagId)
            {
                return NotFound();
            }
            var tag = await this.applicationDbContext.SpecialTags.FindAsync(id);
            if (ModelState.IsValid)
            {
                tag.TagName = tags.TagName;
                await this.applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var Stag = await this.applicationDbContext.SpecialTags.FindAsync(id);
                return View(Stag);
            }

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteFromDatabase(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var tag = await this.applicationDbContext.SpecialTags.FindAsync(id);
            this.applicationDbContext.Remove(tag);
            await this.applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(this.Index));


        }

        [HttpGet]
        public async Task<IActionResult> Details (int ?id )
        {
            if (id == null)
            {
                return NotFound();
            }
           var model = await this.applicationDbContext.SpecialTags.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


    }
}
