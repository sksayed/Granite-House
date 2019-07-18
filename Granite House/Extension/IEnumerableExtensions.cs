using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Granite_House.Extension
{
    public static class IEnumerableExtensions
    {
        public static List<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items)
        {
            //var m = from item in items
            //        select new SelectListItem
            //        {
            //            Text = item.GetPropertyValue("Name"),
            //            Value = item.GetPropertyValue("ID"),
            //           // Selected = items.GetPropertyValue("ID").Equals(SelectedValue.ToString())

            //        };
            //return m;

            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var item in items)
            {
                listItems.Add(new SelectListItem
                {
                    Text = item.GetPropertyValue("Name"),
                    Value = item.GetPropertyValue("ID")

                });
            }
            return listItems;
        }
    }
}
