using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;
using System.Text;

namespace MotorMart.Core.HtmlHelpers
{
    public static class TextEntityExtensions
    {
        public static MvcHtmlString H1(this HtmlHelper htmlHelper, string value)
        {
            return TextEntity(htmlHelper, "h1", value, new RouteValueDictionary());
        }
        public static MvcHtmlString H1(this HtmlHelper htmlHelper, string value, object htmlAttributes)
        {
            return TextEntity(htmlHelper, "h1", value, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString H2(this HtmlHelper htmlHelper, string value)
        {
            return TextEntity(htmlHelper, "h2", value, new RouteValueDictionary());
        }
        public static MvcHtmlString H2(this HtmlHelper htmlHelper, string value, object htmlAttributes)
        {
            return TextEntity(htmlHelper, "h2", value, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString H3(this HtmlHelper htmlHelper, string value)
        {
            return TextEntity(htmlHelper, "h3", value, new RouteValueDictionary());
        }
        public static MvcHtmlString H3(this HtmlHelper htmlHelper, string value, object htmlAttributes)
        {
            return TextEntity(htmlHelper, "h3", value, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString H4(this HtmlHelper htmlHelper, string value)
        {
            return TextEntity(htmlHelper, "h4", value, new RouteValueDictionary());
        }
        public static MvcHtmlString H4(this HtmlHelper htmlHelper, string value, object htmlAttributes)
        {
            return TextEntity(htmlHelper, "h4", value, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString H5(this HtmlHelper htmlHelper, string value)
        {
            return TextEntity(htmlHelper, "h5", value, new RouteValueDictionary());
        }
        public static MvcHtmlString H5(this HtmlHelper htmlHelper, string value, object htmlAttributes)
        {
            return TextEntity(htmlHelper, "h5", value, new RouteValueDictionary(htmlAttributes));
        }

        private static MvcHtmlString TextEntity(this HtmlHelper htmlHelper, string tagName, string value, IDictionary<string, object> htmlAttributes)
        {
            if (htmlAttributes.ContainsKey("id"))
            {
                string id = htmlAttributes["id"] as string;
            }

            TagBuilder tagBuilder = new TagBuilder(tagName);
            tagBuilder.MergeAttributes(htmlAttributes);

            tagBuilder.InnerHtml = Regex.Replace(value, @"&\s", "&amp; ");

            if (value == string.Empty) return MvcHtmlString.Create(value); else return MvcHtmlString.Create(tagBuilder.ToString());
        }

        public static MvcHtmlString HtmlContent(this HtmlHelper helper, string content)
        {
            var sb = new StringBuilder();

            if (content.Trim() == "<br />") content = "";
            sb.Append(content);

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString MaxLength(this HtmlHelper helper, string content, int maxLength, string suffix = "...")
        {
            if (content.Length <= maxLength)
                return MvcHtmlString.Create(content);

            content = content.Substring(0, maxLength).Trim() + suffix;

            return MvcHtmlString.Create(content);
        }
    }
}