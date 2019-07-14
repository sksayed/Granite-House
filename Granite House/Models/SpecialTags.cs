using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Granite_House.Models
{
    public class SpecialTags
    {
        [Required]
        [Key]
        public int TagId { get; set; }

        [Required]
        public string TagName { get; set; }


    }
}
