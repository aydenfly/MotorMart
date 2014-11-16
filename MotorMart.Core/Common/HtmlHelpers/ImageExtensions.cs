using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MotorMart.Core.Models;

namespace MotorMart.Core.HtmlHelpers
{
    public static class ImageExtensions
    {
        public static string ImageFilename(this HtmlHelper helper, string filename, int width)
        {
            StringBuilder sb = new StringBuilder();
            if (width > 0)
            {
                sb.Append(String.Format("{0}_", width));
            }
            sb.Append(filename);
            return sb.ToString();
        }

        public static string RouteLinkImageButton(this HtmlHelper helper, string RouteName, object routeValues, string Directory, string File, string Title, object HtmlAttributes)
        {
            return RouteLinkImageButton(helper, RouteName, routeValues, Directory, File, Title, new RouteValueDictionary(HtmlAttributes));
        }

        private static string RouteLinkImageButton(this HtmlHelper helper, string RouteName, object routeValues, string Directory, string File, string Title, IDictionary<string, object> htmlAttributes)
        {
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);

            TagBuilder tb = new TagBuilder("a");
            tb.MergeAttributes(htmlAttributes);
            tb.MergeAttribute("href", url.RouteUrl(RouteName, routeValues), true);
            tb.MergeAttribute("title", Title, true);
            tb.InnerHtml += Image(helper, Directory, File, Title, new RouteValueDictionary(new object()));

            return tb.ToString();
        }

        public static MvcHtmlString ActionLinkImageButton(this HtmlHelper helper, string controller, string action, object routeValues, string Directory, string File, string Title, object HtmlAttributes)
        {
            return ActionLinkImageButton(helper, controller, action, routeValues, Directory, File, Title, new RouteValueDictionary(HtmlAttributes));
        }

        private static MvcHtmlString ActionLinkImageButton(this HtmlHelper helper, string controller, string action, object routeValues, string Directory, string File, string Title, IDictionary<string, object> htmlAttributes)
        {
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);

            TagBuilder tb = new TagBuilder("a");
            tb.MergeAttributes(htmlAttributes);
            tb.MergeAttribute("href", url.Action(action, controller, routeValues), true);
            tb.MergeAttribute("title", Title, true);
            tb.InnerHtml += Image(helper, Directory, File, Title, new RouteValueDictionary(new object()));

            return MvcHtmlString.Create(tb.ToString());
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string Directory, string File)
        {
            return Image(helper, Directory, File, "", new RouteValueDictionary(new object()));
        }

        public static MvcHtmlString ImageGallery(this HtmlHelper helper, int ItemId, string ThumbDirectory, string GalleryDirectory, string File)
        {
            //<a href="images/image-1.jpg" rel="lightbox[gallery]">image #1</a>

            LinqVehicleRepository _vehicleRepository = new LinqVehicleRepository();

            StringBuilder sb = new StringBuilder();

            if (ItemId > 0)
            {                
                if (!String.IsNullOrEmpty(File))
                {
                    string ThumbImageUrl = ThumbDirectory +  File;
                    string GalleryImageUrl = GalleryDirectory + File;
                    sb.AppendFormat("<a href=\"{0}\" rel=\"lightbox[gallery-{1}]\">", GalleryImageUrl, ItemId);
                    sb.Append(Image(helper, ThumbDirectory, File, "", new RouteValueDictionary(new object())));
                    sb.Append("</a>");

                    vehicle vehicle = _vehicleRepository.GetVehicle(ItemId);
                    if (vehicle.vehicleimages.Count > 0)
                    {
                        var Images = _vehicleRepository.GetVehicleImages(ItemId);
                        foreach (var img in Images)
                        {
                            ThumbImageUrl = ThumbDirectory + img.filename;
                            GalleryImageUrl = GalleryDirectory + img.filename;
                            sb.AppendFormat("<a href=\"{0}\" rel=\"lightbox[gallery-{1}]\" class=\"hidden-image\">", GalleryImageUrl, ItemId);
                            sb.Append(Image(helper, ThumbDirectory, img.filename, "", new RouteValueDictionary(new object())));
                            sb.Append("</a>");
                        }
                    }
                }
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string Directory, string File, object HtmlAttributes)
        {
            return Image(helper, Directory, File, "", new RouteValueDictionary(HtmlAttributes));
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string Directory, string File, string AltText)
        {
            return Image(helper, Directory, File, AltText, new RouteValueDictionary(new object()));
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string Directory, string File, string AltText, object HtmlAttributes)
        {
            return Image(helper, Directory, File, AltText, new RouteValueDictionary(HtmlAttributes));
        }
        
        private static MvcHtmlString Image(this HtmlHelper helper, string Directory, string File, string AltText, IDictionary<string, object> htmlAttributes)
        {
            Directory = Directory.EndsWith("/") ? Directory : Directory + "/";
            Directory = Directory.EndsWith("/") && File == String.Empty ? Directory.Substring(0, Directory.Length - 1) : Directory;
            string ImageSrc = Directory + File;

            TagBuilder tbImg = new TagBuilder("img");
            tbImg.MergeAttributes(htmlAttributes);
            tbImg.MergeAttribute("src", ImageSrc, true);
            tbImg.MergeAttribute("alt", AltText, true);
            tbImg.MergeAttribute("title", AltText, true);

            return MvcHtmlString.Create(tbImg.ToString(TagRenderMode.SelfClosing));
        }
    }
}