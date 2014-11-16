using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorMart.Core.Models;
using MotorMart.Core.Common;
using System.Web.Mvc;

namespace MotorMart.Core.Common
{
    public static class SiteMapHelper
    {
        //public SiteMapHelper()
        //{            
        //}

        public static string GetUrl(IList<sitemap> EntireSitemap, sitemap thispage)
        {
            string url = SiteMapHelper.BuildUrl(EntireSitemap, thispage) + "/" + thispage.reference;
            if (url.StartsWith("/")) url = url.Substring(1);
            return url;
        }

        public static string BuildUrl(IList<sitemap> EntireSitemap, sitemap thispage)
        {
            StringBuilder sb = new StringBuilder();

            var pg = EntireSitemap.Where(p => p.sitemapid == thispage.sitemapparentid && p.reference != "home");
            foreach (sitemap page in pg)
            {                
                sb.Append(BuildUrl(EntireSitemap, page));
                sb.Append("/");
                sb.Append(page.reference);
            }
            
            return sb.ToString();
        }

        public static string GetPathTitle(IList<sitemap> EntireSitemap, sitemap thispage, string PathDelimiter)
        {
            string FullString = BuildTitlePath(EntireSitemap, thispage, PathDelimiter);
            
            if (FullString.EndsWith(" " + PathDelimiter + " ")) FullString = FullString.Substring(0, FullString.Length - (2 + PathDelimiter.Length));

            return FullString;
        }

        public static string BuildTitlePath(IList<sitemap> EntireSitemap, sitemap thispage, string PathDelimiter)
        {
            StringBuilder sb = new StringBuilder();

            var pg = EntireSitemap.Where(p => p.sitemapid == thispage.sitemapparentid && p.reference != "home");
            foreach (sitemap page in pg)
            {
                sb.Append(BuildTitlePath(EntireSitemap, page, PathDelimiter));
                sb.Append(page.title);
                if (PathDelimiter != String.Empty)
                {
                    sb.Append(" " + PathDelimiter);
                }  
                
                sb.Append(" ");
            }

            string FullString = sb.ToString();            

            return FullString;
        }

        public static sitemap GetSiteMapParent(IList<sitemap> EntireSitemap, sitemap thispage)
        {
            return EntireSitemap.Where(p => p.sitemapid == thispage.sitemapparentid).FirstOrDefault();
        }

        public static int ChildCount(sitemap page)
        {
            int count = 0;
            count = page.sitemaps.SelectMany(parent => parent.sitemaps).Count();
            return count;
        }

        public static int ParentCount(sitemap page)
        {
            int count = 0;

            if (page.sitemapparentid == null)
            {
                count = 0;
            }
            else if (page.sitemap1.sitemapparentid == null)
            {
                count = 1;
            }
            else if (page.sitemap1.sitemap1.sitemapparentid == null)
            {
                count = 2;
            }
            else if (page.sitemap1.sitemap1.sitemap1.sitemapparentid == null)
            {
                count = 3;
            }

            return count;
        }

        public static bool PageExists(string _Url, IList<sitemap> EntireSitemap)
        {            
            string[] parts = _Url.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            string validurl = "";

            sitemap parentpage = EntireSitemap.Where(s => s.reference == parts[0]).FirstOrDefault();
            sitemap currentpage;

            if (parentpage != null)
            {
                validurl += parentpage.reference;

                for (int i = 1; i < parts.Length; i++)
                {
                    currentpage = EntireSitemap.Where(u => u.sitemapparentid == parentpage.sitemapid && u.reference == parts[i]).FirstOrDefault();

                    if (currentpage != null)
                    {
                        validurl += "/" + currentpage.reference;
                        parentpage = currentpage;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return (validurl == _Url);
        }

        #region Admin

        public static MvcHtmlString SitemapTreeView(this HtmlHelper html, IList<sitemap> pages, IList<sitemap> allpages)
        {
            var tb = new TagBuilder("div");
            tb.MergeAttribute("id", "sitemap-treeview-wrapper", true);            
            tb.InnerHtml += GenerateTree(html, pages, allpages, 0);
            return MvcHtmlString.Create(tb.ToString());
        }

        private static MvcHtmlString GenerateTree(this HtmlHelper html, IList<sitemap> pages, IList<sitemap> allpages, int LevelCount)
        {
            var sb = new StringBuilder();
            int PagesCount = pages.Count;

            if (PagesCount > 0)
            {
                UrlHelper urlHelper = new UrlHelper(html.ViewContext.RequestContext);

                TagBuilder tb = new TagBuilder("ul");

                if (LevelCount == 0)
                {
                    tb.MergeAttribute("id", "sitemap-treeview", true);
                }

                LevelCount++;

                foreach (sitemap page in pages)
                {
                    TagBuilder tbLi = new TagBuilder("li");
                    //tbLi.InnerHtml += html.ActionLink(page.title, "edit", "sitemap", new { @sitemapid = page.sitemapid }, new { });

                    TagBuilder tbAnchor = new TagBuilder("a");
                    tbAnchor.MergeAttribute("title", page.title);
                    tbAnchor.MergeAttribute("href", urlHelper.Action("edit", "sitemap", new { @area = "sitemap", @sitemapid = page.sitemapid}));
                    tbAnchor.SetInnerText(page.title);
                    tbLi.InnerHtml += tbAnchor.ToString();

                    tbLi.InnerHtml += GenerateTree(html, page.sitemaps.OrderBy(c => c.sortorder).ToList(), allpages, LevelCount);

                    tb.InnerHtml += tbLi.ToString();
                }
                sb.Append(tb.ToString());
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString SitemapPath(sitemap CurrentSitemap, IList<sitemap> allpages)
        {
            var sb = new StringBuilder();
            
            if (CurrentSitemap == null) CurrentSitemap = new sitemap();

            if (CurrentSitemap.sitemapparentid != null)
            {
                sb.Append(SitemapPath(allpages.Where(s => s.sitemapid == CurrentSitemap.sitemapparentid).FirstOrDefault(), allpages));
            }
            sb.Append(" > " + CurrentSitemap.title);
            return MvcHtmlString.Create(sb.ToString());
        }

        #endregion
    }
}
