using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Granite_House.Models.ViewModel
{
    public class MovieViewModel
    {
        public IList<Movie> Movies { get; set; }
        public SelectList GenereSelectList { get; set; }

        public string MovieGenere { get; set; }

        public string SearchString { get; set; }
    }
}
