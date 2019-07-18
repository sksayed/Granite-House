using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Granite_House.Extension
{
    public static class ReflectionExtension
    {
        public static string GetPropertyValue<T>(this T item, string propertyName)
        {
            var a = item.GetType();
            var b = a.GetProperty(propertyName);
            var c = b.GetValue(item, null);
            var d = c.ToString();

            return d;


            //return item.GetType().GetProperty(propertyName).GetValue(item).ToString();
            //return item.GetType().GetProperty(propertyName).GetValue(item, null).ToString();
        }
    }
}
