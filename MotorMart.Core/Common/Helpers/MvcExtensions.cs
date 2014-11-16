using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
    public static class MvcExtensions
    {
        public static List<SelectListItem> ToSelectList<T>(
            this IEnumerable<T> enumerable,
            Func<T, string> text,
            Func<T, string> value,
            Func<T, bool> isSelected,
            string defaultOption)
        {
            var items = enumerable.Select(f => new SelectListItem()
            {
                Text = text(f),
                Value = value(f),
                Selected = isSelected(f)
            }).ToList();
            if (!String.IsNullOrEmpty(defaultOption))
            {
                items.Insert(0, new SelectListItem()
                {
                    Text = defaultOption,
                    Value = ""
                });
            }
            return items;
        }
    }
}
