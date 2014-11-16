using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;
using System.Web.Mvc;
using MotorMart.Core.Common;
using System.Text;

namespace MotorMart.Web.HtmlHelpers
{
    public static class MenuHelper
    {
        private static IList<sitemap> SitemapList;
        private static int _currentLevel;

        public static MvcHtmlString PrimaryMenu(this HtmlHelper helper, IList<sitemap> _SitemapList)
        {
            //<ul>
            //    <li class="first selected"><a href="">Services for business</a></li>
            //    <li><a href="">Services for you</a></li>
            //    <li><a href="">About</a></li>
            //    <li><a href="">News &amp Events</a></li>
            //    <li><a href="">Contact us</a></li>
            //    <li class="last"><a href="">Recruitment</a></li>
            //</ul>

            SitemapList = _SitemapList;

            sitemap HomeSitemap = SitemapList.Where(a => a.reference == "home" && a.sitemapparentid == null).FirstOrDefault();

            TagBuilder tbContainerUl = new TagBuilder("ul");

            TagBuilder tbItem = new TagBuilder("li");
            tbItem.InnerHtml += GetItem(HomeSitemap);
            tbContainerUl.InnerHtml += tbItem.ToString();

            int count = 0;
            var items = HomeSitemap.sitemaps.Where(a => a.enabled && a.menuvisible).OrderBy(a => a.sortorder);
            int itemCount = items.Count();

            foreach (var item in items)
            {
                string url = item.overrideurl != String.Empty ? item.overrideurl : "/" + SiteMapHelper.GetUrl(SitemapList, item);

                tbItem = new TagBuilder("li");

                if (IsInUrlPath(url))
                {
                    tbItem.InnerHtml += GetItem(item);
                    tbItem.AddCssClass("expanded");
                }
                else
                {
                    tbItem.InnerHtml += GetItem(item);
                }

                if (count == 0)
                {
                    tbItem.AddCssClass("first");
                }
                if (count == itemCount - 1)
                {
                    tbItem.AddCssClass("last");
                }

                tbContainerUl.InnerHtml += tbItem.ToString();

                count++;
            }

            return MvcHtmlString.Create(tbContainerUl.ToString());
        }

        public static MvcHtmlString SecondaryMenu(this HtmlHelper helper, IList<sitemap> _SitemapList, sitemap _currentSitemap)
        {
            SitemapList = _SitemapList;
            _currentLevel = 0;
            return GenerateSecondaryMenu(helper, _currentSitemap);
        }

        public static MvcHtmlString GenerateSecondaryMenu(this HtmlHelper helper, sitemap _currentSitemap)
        {
            if (_currentSitemap == null) return MvcHtmlString.Create("");

            sitemap SectionRootSitemap = _currentSitemap;

            if (SectionRootSitemap.sitemap1.sitemapparentid != null)
            {
                SectionRootSitemap = SectionRootSitemap.sitemap1;

                if (SectionRootSitemap.sitemap1.sitemapparentid != null)
                {
                    SectionRootSitemap = SectionRootSitemap.sitemap1;
                }
            }

            return MvcHtmlString.Create(AddMenuItems(_currentSitemap, SectionRootSitemap));
        }

        private static string AddMenuItems(sitemap _currentSitemap, sitemap SectionRootSitemap)
        {
            _currentLevel += 1;

            TagBuilder tbContainerUl = new TagBuilder("ul");
            tbContainerUl.AddCssClass(String.Format("secondary-menu-level{0}", SectionRootSitemap.level));

            var tbLiItems = new List<TagBuilder>();

            TagBuilder tbItem = new TagBuilder("li");

            if (_currentLevel == 1)
            {
                // Put the root sitemap to the top of the list
                tbItem = new TagBuilder("li");
                tbItem.InnerHtml += GetItem(SectionRootSitemap);
                tbLiItems.Add(tbItem);
            }

            // Add child sitemaps
            string sitemapUrl;

            var items = SectionRootSitemap.sitemaps.Where(a => a.enabled && a.menuvisible).OrderBy(a => a.sortorder);
            int itemCount = items.Count();

            for (int i = 0; i < itemCount; i++)
            {
                tbItem = new TagBuilder("li");

                var item = items.ElementAt(i) as sitemap;

                sitemapUrl = item.overrideurl != String.Empty ? item.overrideurl : "/" + SiteMapHelper.GetUrl(SitemapList, item);

                if (IsInUrlPath(sitemapUrl))
                {
                    tbItem.InnerHtml += GetItem(item);
                    tbItem.AddCssClass("expanded");
                }
                else
                {
                    tbItem.InnerHtml += GetItem(item);
                }

                tbItem.InnerHtml += AddMenuItems(_currentSitemap, item);

                tbLiItems.Add(tbItem);
            }

            // Add the items to the menu
            for (int i = 0; i < tbLiItems.Count; i++)
            {
                if (i == 0)
                {
                    tbLiItems.ElementAt(i).AddCssClass("first");
                }

                if (i == tbLiItems.Count - 1)
                {
                    tbLiItems.ElementAt(i).AddCssClass("last");
                }
            }

            tbLiItems.ForEach(a => tbContainerUl.InnerHtml += a.ToString());

            if (tbContainerUl.InnerHtml != String.Empty) return tbContainerUl.ToString();

            return String.Empty;
        }

        private static TagBuilder GetLi(string sitemapUrl, string itemReference, string itemName, out string url, string additionalCss = null)
        {
            url = String.Format("{0}/{1}", sitemapUrl, itemReference);

            TagBuilder tbLi = new TagBuilder("li");

            TagBuilder tbAnchor = new TagBuilder("a");
            tbAnchor.MergeAttribute("href", url);
            tbAnchor.InnerHtml += HttpUtility.HtmlEncode(itemName);
            AddAnchorCssClasses(tbAnchor, url);

            if (additionalCss != null)
            {
                tbAnchor.AddCssClass(additionalCss);
            }

            tbLi.InnerHtml += tbAnchor.ToString();

            return tbLi;
        }

        private static string GetItem(sitemap Sitemap, string cssClass = null)
        {
            string url = Sitemap.overrideurl != String.Empty ? Sitemap.overrideurl.ToLower() : "/" + SiteMapHelper.GetUrl(SitemapList, Sitemap).ToLower();

            url = url == "/home" ? url = "/" : url;

            StringBuilder sb = new StringBuilder();

            TagBuilder tbAnchor = new TagBuilder("a");
            tbAnchor.MergeAttribute("href", url, true);
            if (cssClass != null)
            {
                tbAnchor.AddCssClass(cssClass);
            }

            AddAnchorCssClasses(tbAnchor, url);

            string linkText = Sitemap.menudisplayname != String.Empty ? HttpUtility.HtmlEncode(Sitemap.menudisplayname) : HttpUtility.HtmlEncode(Sitemap.title);
            tbAnchor.InnerHtml += linkText;

            sb.Append(tbAnchor.ToString());

            return sb.ToString();
        }

        private static void AddAnchorCssClasses(TagBuilder tag, string url)
        {
            if (IsInUrlPath(url))
            {
                tag.AddCssClass("expanded");
            }
            if (url == HttpContext.Current.Request.CurrentExecutionFilePath.ToLower())
            {
                tag.AddCssClass("selected");
            }
        }

        private static bool IsInUrlPath(string url)
        {
            bool valid = false;

            string[] parts = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            string[] requestParts = HttpContext.Current.Request.CurrentExecutionFilePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < requestParts.Length; i++)
            {
                if (parts.Length > i)
                {
                    if (requestParts[i] == parts[i])
                    {
                        valid = true;
                    }
                    else
                    {
                        valid = false;
                    }
                }
            }

            if (parts.Length > requestParts.Length)
            {
                valid = false;
            }

            return valid;
        }
    }
}