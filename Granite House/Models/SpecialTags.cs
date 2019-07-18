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
        [MinLength(3,ErrorMessage =" You Must Input 3 characters ")]
        [Required]
        public string TagName { get; set; }


    }
}
