using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Granite_House.Models
{
    public class ProductTypes
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
