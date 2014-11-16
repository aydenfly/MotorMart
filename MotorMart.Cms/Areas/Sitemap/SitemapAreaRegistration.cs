using System.Web.Mvc;

namespace MotorMart.Cms.Areas.Sitemap
{
    public class SitemapAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Sitemap";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            string Namespace = "MotorMart.Cms.Areas.Sitemap.Controllers";

            #region Sitemap

            context.MapRoute(
                "SitemapDeleteDialog",
                "sitemap/deletedialog/{sitemapid}",
                new { controller = "Sitemap", action = "SitemapDeleteDialog", sitemapid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "SitemapDelete",
                "sitemap/edit/{sitemapid}/delete",
                new { controller = "Sitemap", action = "Delete", sitemapid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "SitemapStaticContentEdit",
                "sitemap/edit/{sitemapid}/staticcontent",
                new { controller = "Sitemap", action = "EditStaticContent", sitemapid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );            

            context.MapRoute(
                "SitemapEdit",
                "sitemap/edit/{sitemapid}",
                new { controller = "Sitemap", action = "Edit", sitemapid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "SitemapAdd",
                "sitemap/add",
                new { controller = "Sitemap", action = "Add"},
                null,
                new string[] { Namespace }
                );
            
            context.MapRoute(
                "Sitemap_default",
                "sitemap/{controller}/{action}/{id}",
                new { area ="Sitemap", controller = "Sitemap", action = "Index", id = UrlParameter.Optional }
            );
            #endregion
        }
    }
}
