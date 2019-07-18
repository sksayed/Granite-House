using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Granite_House.Models.ViewModel
{
    /// <summary>
    /// this is a DTO
    ///this is also called
    ///as data transfer object , this will be rendered into the view as 
    ///view can only handle only one model 
    /// </summary>
    public class ProductsViewModels
    {
        public Products Products { get; set; }

        public IEnumerable<SpecialTags> SpecialTags { get; set; }

        public List<SelectListItem> selectLists { get; set; }

        public IEnumerable<ProductTypes> ProductTypes { get; set; }


    }
}