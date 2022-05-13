using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ArmyTech.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItemName<T>(this IEnumerable<T> items, int? selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("NameArab"),
                       Value = item.GetPropertyValue("BaseId"),
                       Selected = item.GetPropertyValue("BaseId").Equals(selectedValue.ToString())
                   };
        }
    }
}
