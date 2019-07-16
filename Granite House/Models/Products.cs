using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Granite_House.Models
{
    public class Products
    {
        [Required]
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        [Required]
        public double Price { get; set; }

        public bool Available { get; set; }

        public string Image { get; set; }

        public string ShadeColor { get; set; }

        //we will use dataannotations to configure 
        //we will use fluent Api in future to configure the 
        //database table 
        [Display(Name = "Product Type")]
        public int ProductTypesId { get; set; }

        [ForeignKey(name: "ProductTypesId")]
        public virtual ProductTypes ProductTypes { get; set; }
        //here virtual keyword has been used so that 
        //it will not be added inside the database 

        //we will use dataannotations to configure 
        //we will use fluent Api in future to configure the 
        //database table 
        [Display(Name = "Special Tags")]
        public int SpecialTagsId { get; set; }

        [ForeignKey(name: "SpecialTagsId")]
        public virtual SpecialTags SpecialTags { get; set; }
        //here virtual keyword has been used so that 
        //it will not be added inside the database 

    }
}
