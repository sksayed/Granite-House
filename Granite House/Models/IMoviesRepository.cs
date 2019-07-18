using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Granite_House.Models
{
    interface IMoviesRepository
    {
        IEnumerable<Movie> MovieList();
    }
}


