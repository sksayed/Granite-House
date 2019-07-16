using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Granite_House.Extension
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<SelectItemList> ToSelectListItem<T>(this IEnumerable<T> items, int SelectedValue)
        {
            return from item in items
                   select new SelectItemList
                   {
                       Text = item.GetPropertyValue("Name"),
                       Value = item.GetPropertyValue("ID"),
                       Selected = items.GetPropertyValue("ID").Equals(SelectedValue.ToString())

                   };
        }
    }
}
