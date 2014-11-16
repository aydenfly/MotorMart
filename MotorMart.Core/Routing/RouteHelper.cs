using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MotorMart.Core.Models;
using MotorMart.Core.Models.Validation;
using MotorMart.Core.Services;
using MotorMart.Core.Common;
using System.Reflection;

namespace MotorMart.Core.Routing
{
    public class RouteHelper
    {
        const string ApplicationRouteNamespace = "MotorMart.Web.Controllers";
        static IList<sitemap> EntireSitemap;
        static MasterService _service;

        public RouteHelper()
        {
            _service = new MasterService(new ModelStateWrapper(new ModelStateDictionary()));
        }

        public RouteHelper(MasterService service)
        {
            _service = service;
        }

        public static RouteHelper Instance
        {
            get
            {
                return new RouteHelper();
            }
        }
        
        public void UpdateRouteRegistration()
        {
            UpdateRouteRegistration(false);
        }

        public void UpdateRouteRegistration(bool UpdateRoutesRegardlessOfSettings)
        {  
            if ((UpdateRoutesRegardlessOfSettings) || GlobalSettings.UpdateRoutes)
            {
                RouteCollection routes = RouteTable.Routes;

                using (routes.GetWriteLock())
                {
                    routes.Clear();
                    GetSitemaps();
                    IgnoreRoutes(routes);
                    AreaRegistration.RegisterAllAreas();
                    RegisterRoutesFromDB(routes);
                    AddCustomRoutes(routes);
                    AddDefaultRoute(routes);
                }

                //Uncomment this to test Url Routing
                //RouteDebug.RouteDebugger.RewriteRoutesForTesting(routes);
            }
        }

        private void GetSitemaps()
        {
            MasterService _service = new MasterService(new ModelStateWrapper(new ModelStateDictionary()));
            EntireSitemap = _service.ListSitemap();
        }

        public RouteCollection GetRouteCollection(IList<sitemap> entireSitemap)
        {
            EntireSitemap = entireSitemap;

            RouteCollection collection = new RouteCollection();
            RegisterRoutesFromDB(collection);
            return collection;
        }

        public void IgnoreRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
        }

        public void RegisterRoutesFromDB(RouteCollection routes)
        {
            List<string> Controllers = new List<string>
            {
                "HOME"
            };

            foreach (sitemap item in EntireSitemap.Where(c => c.enabled && !Controllers.Contains(c.controller.ToUpper())).OrderByDescending(c => c.level).ToList())
            {
                if (item.routename.ToUpper() == "USEDCARS" || item.routename.ToUpper() == "NEWCARS")
                {
                    routes.Add(
                            item.routename,
                            new Route(
                                SiteMapHelper.GetUrl(EntireSitemap, item) +
                                "/{postcode}/{makeid}/{modelid}/{fueltypeid}/{vehicleage}/{vehiclemileage}/{dealerdistance}/{enginezine}/{bodytypeid}/{transmissionid}/{numberofdoors}/{minprice}/{maxprice}/{colorid}/{sortby}",
                                new RouteValueDictionary(new
                                {
                                    controller = item.controller,
                                    action = item.action == string.Empty ? "Index" : item.action,
                                    Sitemap = item,
                                    postcode = UrlParameter.Optional,
                                    makeid = UrlParameter.Optional,
                                    modelid = UrlParameter.Optional,
                                    fueltypeid = UrlParameter.Optional,
                                    vehicleage = UrlParameter.Optional,
                                    vehiclemileage = UrlParameter.Optional,
                                    dealerdistance = UrlParameter.Optional,
                                    enginezine = UrlParameter.Optional,
                                    bodytypeid = UrlParameter.Optional,
                                    transmissionid = UrlParameter.Optional,
                                    numberofdoors = UrlParameter.Optional,
                                    minprice = UrlParameter.Optional,
                                    maxprice = UrlParameter.Optional,
                                    colorid = UrlParameter.Optional,
                                    sortby = UrlParameter.Optional
                                }),
                                new RouteValueDictionary(new
                                {
                                    Sitemap = new SitemapRouteConstraint(_service)
                                }),
                                new RouteValueDictionary(new
                                {
                                    namespaces = new string[] { item.routenamespace }
                                }),
                                new DataBinderRouteHandler()
                            ));
                }
                else if (item.routename.ToUpper() == "CONFIRMENQUIRY")
                {
                    routes.Add(
                    item.routename,
                    new Route(
                        SiteMapHelper.GetUrl(EntireSitemap, item) +
                        "/{firstname}/{lastname}/{email}/{telephone}",
                        new RouteValueDictionary(new
                        {
                            controller = item.controller,
                            action = item.action,
                            Sitemap = item,
                            fn = UrlParameter.Optional,
                            firstname = UrlParameter.Optional,
                            ln = UrlParameter.Optional,
                            lastname = UrlParameter.Optional,
                            em = UrlParameter.Optional,
                            email = UrlParameter.Optional,
                            tel = UrlParameter.Optional,
                            telephone = UrlParameter.Optional
                        }),
                        new RouteValueDictionary(new
                            {
                                Sitemap = new SitemapRouteConstraint(_service)
                            }),
                            new RouteValueDictionary(new
                            {
                                namespaces = new string[] { item.routenamespace }
                            }),
                            new DataBinderRouteHandler()
                        ));
                }
                else
                {
                    routes.Add(
                            item.routename,
                            new Route(
                                SiteMapHelper.GetUrl(EntireSitemap, item),
                                new RouteValueDictionary(new
                                {
                                    controller = item.controller,
                                    action = item.action == string.Empty ? "Index" : item.action,
                                    Sitemap = item
                                }),
                                new RouteValueDictionary(new
                                {
                                    Sitemap = new SitemapRouteConstraint(_service)
                                }),
                                new RouteValueDictionary(new
                                {
                                    namespaces = new string[] { item.routenamespace }
                                }),
                                new DataBinderRouteHandler()
                            ));
                }
            }

        }       

        public static string GetUrl(IList<sitemap> EntireSitemap, sitemap thispage)
        {
            string url = SiteMapHelper.BuildUrl(EntireSitemap, thispage) + "/" + thispage.reference;
            if (url.StartsWith("/")) url = url.Substring(1);
            return url;
        }

        public void AddRouteValues(RouteValueDictionary dictionary)
        {
            if (dictionary["Sitemap"] != null)
            {
                // Refresh the entity
                sitemap Sitemap = dictionary["Sitemap"] as sitemap;

                if (Sitemap != null)
                {
                    RouteDataBinder binder = new RouteDataBinder
                    {
                        Sitemap = Sitemap                        
                    };
                    
                    dictionary.Add("RouteDataBinder", binder);
                }
            }
        }

        public void AddDefaultRoute(RouteCollection routes)
        {
            sitemap RootSitemap = EntireSitemap.Where(s => s.sitemapparentid == null).FirstOrDefault();
            
            //string ItemAction = RootSitemap.action == String.Empty ? "Index" : RootSitemap.action;
            string RouteTitle = RootSitemap.routename;
            string RouteNamespaces = RootSitemap.routename == String.Empty ? ApplicationRouteNamespace : RootSitemap.routenamespace;

            routes.Add(
                RouteTitle,
                new Route(
                    "{controller}",
                    new RouteValueDictionary(new
                    {
                        controller = "Home",
                        action = "Index",
                        Sitemap = RootSitemap
                    }),
                    new RouteValueDictionary(new
                        {
                        }),
                        new RouteValueDictionary(new
                        {
                            namespaces = new string[] { RouteNamespaces }
                        }),
                        new DataBinderRouteHandler()
                        ));

           
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { ApplicationRouteNamespace }
            );            

            routes.MapRoute(
                "NotFound",
                "{*catchall}",
                new { controller = "Error", action = "Http404" }
            );
        }

        public void AddCustomRoutes(RouteCollection routes)
        {
            //To add non-db-based routes
            //routes.Add(
            //    "ConfirmEnquiry",
            //    new Route(
            //        "contact-us/thank-you/firstname/{firstname}/lastname/{lastname}/email/{email}/telephone/{telephone}",
            //        new RouteValueDictionary(new
            //        {
            //            controller = "Contact",
            //            action = "Confirm",
            //            firstname = UrlParameter.Optional,
            //            lastname = UrlParameter.Optional,
            //            email = UrlParameter.Optional,
            //            telephone = UrlParameter.Optional
            //        }),
            //        new RouteValueDictionary(new
            //        {
            //        }),
            //            new RouteValueDictionary(new
            //            {
            //                namespaces = new string[] { ApplicationRouteNamespace }
            //            }),
            //            new DataBinderRouteHandler()
            //            ));
        }

    }

    public class DataBinderRouteHandler : IRouteHandler
    {
        public DataBinderRouteHandler()
        {
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            RouteData routeData = requestContext.RouteData;
            RouteHelper.Instance.AddRouteValues(routeData.Values);
            return new MvcHandler(requestContext);
        }
    }

    public class SitemapRouteConstraint : IRouteConstraint
    {
        MasterService _service;

        public SitemapRouteConstraint(MasterService service)
        {
            _service = service;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.UrlGeneration) return true;

            if (parameterName.ToLower() == "sitemap")
            {
                var value = values["sitemap"] as sitemap;

                if (value != null)
                {
                    return _service.GetSitemap(value.sitemapid).enabled;
                }
            }

            return false;
        }

    }
}
