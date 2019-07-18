using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Granite_House.Models
{
    public class MovieRepository : IMoviesRepository
    {
        private readonly IEnumerable<Movie> _movies;

        public MovieRepository()
        {
            var list = new List<Movie>()
            {

                   new Movie
                   {
                       Title = "When Harry Met Sally",
                       ReleaseDate = DateTime.Parse("1989-2-12"),
                       Genere = "Romantic Comedy",
                       Price = 7.99
                   },

                   new Movie
                   {
                       Title = "Ghostbusters ",
                       ReleaseDate = DateTime.Parse("1984-3-13"),
                       Genere = "Comedy",
                       Price = 8.99
                   },

                   new Movie
                   {
                       Title = "Ghostbusters 2",
                       ReleaseDate = DateTime.Parse("1986-2-23"),
                       Genere = "Comedy",
                       Price = 9.99
                   },

                   new Movie
                   {
                       Title = "Rio Bravo",
                       ReleaseDate = DateTime.Parse("1959-4-15"),
                       Genere = "Western",
                       Price = 3.99
                   }
                 };
            this._movies = list;

        }

        public IEnumerable<Movie> MovieList()
        {
            return this._movies;


        }
    }
}
