using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace MotorMart.Core.HtmlHelpers
{
    public static class LinkExtensions
    {
        public static string ActionQueryLink(this HtmlHelper htmlHelper, string linkText, string action)
        {
            return ActionQueryLink(htmlHelper, linkText, action, null, null, null);
        }

        public static string ActionQueryLink(this HtmlHelper htmlHelper, string linkText, string action, object routeValues)
        {
            return ActionQueryLink(htmlHelper, linkText, action, null, new RouteValueDictionary(routeValues), null);
        }

        public static string ActionQueryLink(this HtmlHelper htmlHelper, string linkText, string action, RouteValueDictionary routeValues)
        {
            return ActionQueryLink(htmlHelper, linkText, action, null, routeValues, null);
        }

        public static string ActionQueryLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes)
        {
            return ActionQueryLink(htmlHelper, linkText, null, routeName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
        }

        public static string ActionQueryLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues, object htmlAttributes)
        {
            return ActionQueryLink(htmlHelper, linkText, null, routeName, routeValues, new RouteValueDictionary(htmlAttributes));
        }

        public static string RouteQueryLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, object htmlAttributes)
        {
            return ActionQueryLink(htmlHelper, linkText, actionName, null, routeValues, new RouteValueDictionary(htmlAttributes));
        }

        public static string ActionQueryLink(this HtmlHelper htmlHelper, string linkText, string action, string routeName, RouteValueDictionary routeValues, RouteValueDictionary htmlAttributes)
        {
            var queryString = htmlHelper.ViewContext.HttpContext.Request.QueryString;

            var newRoute = routeValues == null
                ? htmlHelper.ViewContext.RouteData.Values
                : routeValues;

            foreach (string key in queryString.Keys)
            {
                if (!newRoute.ContainsKey(key))
                {
                    string value = queryString[key];
                    if (value.Contains(","))
                    {
                        value = value.Substring(0, value.IndexOf(","));
                    }
                    newRoute.Add(key, value);
                }
            }

            return HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext,
                htmlHelper.RouteCollection, linkText, routeName,
                action, null, newRoute, htmlAttributes);
        }

        public static string RssLink(this HtmlHelper htmlHelper, string routeName, object routeValues)
        {
            //return RssLink(htmlHelper, "Subscribe RSS", routeName, routeValues).ToString();
            return GenerateRssLink(htmlHelper, "Subscribe RSS", routeName, routeValues, null).ToString();
        }

        public static string RssLink(this HtmlHelper htmlHelper, string routeName, object routeValues, object htmlAttributes)
        {
            return GenerateRssLink(htmlHelper, "Subscribe RSS", routeName, routeValues, new RouteValueDictionary(htmlAttributes)).ToString();
        }

        public static string RssLink(this HtmlHelper htmlHelper, string RssSubscribeText, string routeName, object routeValues)
        {
            //return htmlHelper.RouteLink(RssSubscribeText, routeName, routeValues, new { @class = "icon-rss", @target = "_blank" }).ToString();
            return GenerateRssLink(htmlHelper, RssSubscribeText, routeName, routeValues, null).ToString();
        }

        private static string GenerateRssLink(this HtmlHelper htmlHelper, string RssSubscribeText, string routeName, object routeValues, IDictionary<string, object> htmlAttributes)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            string url = urlHelper.RouteUrl(routeName, routeValues);

            TagBuilder tb = new TagBuilder("a");
            tb.MergeAttribute("href", url, true);
            tb.MergeAttribute("target", "_blank", true);
            tb.MergeAttributes(htmlAttributes, true);
            tb.AddCssClass("icon-rss");
            tb.InnerHtml = RssSubscribeText;
            return tb.ToString(TagRenderMode.Normal);
        }

        public static string RssMetaLink(this HtmlHelper htmlHelper, string routeName, object routeValues)
        {
            return RssMetaLink(htmlHelper, "rss", routeName, routeValues);
        }

        public static string RssMetaLink(this HtmlHelper htmlHelper, string title, string routeName, object routeValues)
        {
            /*
             <link type="application/rss+xml" rel="alternate" title="rss" href="/path/to/rss.xml"  />
            */

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            string url = urlHelper.RouteUrl(routeName, routeValues);

            TagBuilder tb = new TagBuilder("link");
            tb.MergeAttribute("type", "application/rss+xml", true);
            tb.MergeAttribute("rel", "alternate", true);
            tb.MergeAttribute("title", title, true);
            tb.MergeAttribute("href", url, true);
            return tb.ToString(TagRenderMode.SelfClosing);
        }
    }
}