using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Granite_House.Extension
{
    public static  class EnnumerableExample
    {
        public static List<SelectListItem> TogetSelectListOnIdAndData<T>(this IEnumerable<T> items, string id, string text)
        {
            var list = new List<SelectListItem>();

            foreach ( var item in items)
            {
                list.Add(new SelectListItem
                {
                    Text = item.GetPropertyValue(text),
                    Value = item.GetPropertyValue(id)
                });

            }
            return list;

        }
    }
}
