using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Granite_House.Data;
using Granite_House.Models;
using Granite_House.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Granite_House.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _applicationDb;
        public MovieController(ApplicationDbContext applicationDb)
        {
            this._applicationDb = applicationDb;
        }


        //public IActionResult Index()
        //{
        //    try
        //    {
        //        var movies = this._applicationDb.Movies.ToList();
        //        if (movies.Count == 0)
        //        {
        //            throw new ArgumentNullException();
        //        }
        //    }
        //    catch (ArgumentNullException ex)
        //    {
        //        //todo 
        //        //IMovieRepo theke value ene ekhane add korte hbe
        //        Console.WriteLine(ex.Message);
        //        var movieRepo = new MovieRepository();
        //        this._applicationDb.Movies.AddRange(movieRepo.MovieList());
        //        this._applicationDb.SaveChanges();

        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(this._applicationDb.Movies.ToList());
        //}

        [HttpGet]
        public async Task<IActionResult> Index (string SearchString , string MovieGenere )
        {
            var movie = from m in this._applicationDb.Movies select m;
            if (!String.IsNullOrWhiteSpace(SearchString))
            {
                movie = movie.Where(m => m.Title.Contains(SearchString));
            }
            var genereString = from m in this._applicationDb.Movies
                               orderby m.Genere
                               select m.Genere;
            if (!String.IsNullOrWhiteSpace(MovieGenere))
            {
                movie = movie.Where(m => m.Genere == MovieGenere);

            }

            var MovieVM = new MovieViewModel()
            {
                Movies = await movie.ToListAsync(),
                GenereSelectList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await genereString.Distinct().ToListAsync())
            };

            return View(MovieVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit (int ? id  )
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = await this._applicationDb.Movies.FindAsync(id);
            if (ModelState.IsValid)
            {
                
                if (movie == null)
                {
                    return NotFound();

                }
            }
            return View(movie);
        }
    }
}
