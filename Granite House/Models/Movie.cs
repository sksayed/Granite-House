using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Granite_House.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public string Genere { get; set; }
    }
}
