using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using MotorMart.Core.Routing;
using MotorMart.Core.Models;

namespace MotorMart.Core.Common
{
    public static class AreaRouteHelper
    {
        public static void MapAreas(this RouteCollection routes, string url, string rootNamespace)
        {

            try
            {
                DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("Areas"));

                foreach (DirectoryInfo g in dir.GetDirectories())
                {
                    string areaNamespace = rootNamespace + ".Areas." + g.Name + ".Controllers";

                    Route route = new Route("{area}/" + url, new MvcRouteHandler());

                    route.Constraints = new RouteValueDictionary(
                    new
                    {
                        area = g.Name,
                        controller = new SitemapRouteConstraint(areaNamespace)
                    });

                    route.DataTokens = new RouteValueDictionary(
                    new
                    {
                        namespaces = new string[] { areaNamespace }
                    });

                    route.Defaults = new RouteValueDictionary(
                    new
                    {
                        action = "Index",
                        controller = "Home",
                        id = ""
                    });

                    routes.Add(route);
                }
            }
            catch
            {
            }
        }

        public static void MapRootArea(this RouteCollection routes, string url, string rootNamespace, object defaults) {

            string FullNamespace = rootNamespace + ".Controllers";

            MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();
            sitemap RootSitemap = new sitemap();
            try
            {
                RootSitemap = _datacontext.sitemaps.Where(s=>s.sitemapparentid == null).FirstOrDefault();
            }
            catch
            {
            }

            Route route = new Route(url, new MvcRouteHandler());
            
            route.Constraints = new RouteValueDictionary(
            new
            {
                controller = new SitemapRouteConstraint(FullNamespace)
            });

            route.DataTokens = new RouteValueDictionary(
            new 
            {
                namespaces = new string[] { FullNamespace } 
            });

            route.Defaults = new RouteValueDictionary(
            new 
            {
                area="root", 
                action = "Index", 
                controller = "Home", 
                id = "",
                RouteDataBinder = new RouteDataBinder
                { 
                    Sitemap = RootSitemap 
                }
            });

            routes.Add(route);
        }
    }
}
