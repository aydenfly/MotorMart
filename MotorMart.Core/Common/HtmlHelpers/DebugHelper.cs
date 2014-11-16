using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MotorMart.Core.Models;

namespace MotorMart.Core.HtmlHelpers
{
    public static class DebugHelper
    {
        public static string Debug(this HtmlHelper helper, MasterViewModel Model)
        {
            var sb = new StringBuilder();

            if (Model.CurrentUserAccount != null && Model.CurrentUserAccount.useraccountid > 0 && Model.CurrentUserAccount.usergroup.name.ToLower() == "admin")
            {
                sb.Append("<div id=\"debug-window\">");

                if (Model != null)
                {
                    sb.Append("<h3>Route data</h3>");
                    if (Model._controller != null)
                    {
                        sb.Append("<ul>");
                        foreach (var item in Model._controller.RouteData.Values)
                        {
                            sb.Append(String.Format("<li><span>{0}: </span>{1}</li>", item.Key, item.Value));
                        }
                        foreach (var item in Model._controller.RouteData.DataTokens)
                        {
                            sb.Append(String.Format("<li><span>{0}: </span>{1}</li>", item.Key, item.Value));
                        }
                        //sb.Append(String.Format("<li><span>Route name: </span>{0}</li>", Model._controller.RouteData.ToString()));
                        sb.Append("</ul>");
                    }

                    sb.Append("<h3>Global data</h3>");
                    sb.Append("<ul>");                    
                    sb.Append(String.Format("<li><span>Session User Id: </span>{0}</li>", Model.SessionManager.UserAccountId));
                    sb.Append(String.Format("<li><span>Session Id: </span>{0}</li>", Model.SessionManager.SessionId));
                    

                    if (Model.CurrentSitemap != null)
                    {
                        sb.Append(String.Format("<li><span>Sitemap: </span>{0}</li>", Model.CurrentSitemap.sitemapid));
                        sb.Append(String.Format("<li><span>Sitemap Static content: </span>{0}</li>", Model.CurrentSitemap.staticcontentid));
                        
                    }

                    sb.Append(String.Format("<li><span>Current Content ItemId: </span>{0}</li>", Model._controller.CurrentItemId));


                    if (Model._controller.RouteDataBinder != null)
                    {
                        if (Model._controller.RouteDataBinder.Sitemap != null)
                        {
                            sb.Append(String.Format("<li><span>Sitemap Id: </span>{0}</li>", Model._controller.RouteDataBinder.Sitemap.sitemapid));
                            sb.Append(String.Format("<li><span>Sitemap ref: </span>{0}</li>", Model._controller.RouteDataBinder.Sitemap.reference));
                            sb.Append(String.Format("<li><span>Sitemap Route Name: </span>{0}</li>", Model._controller.RouteDataBinder.Sitemap.routename));
                        }
                    }                    

                    sb.Append("</ul>");


                    // COOKIES
                    sb.Append("<h3>Cookies</h3>");
                    if (Model.AppCookies != null)
                    {
                        sb.Append("<ul>");
                        sb.Append(String.Format("<li><span>Cookie Domain: </span>{0}</li>", MotorMart.Core.Common.GlobalSettings.CookieDomain));
                        sb.Append(String.Format("<li><span>User Account Id: </span>{0}</li>", Model.AppCookies.UserAccountId));
                        sb.Append(String.Format("<li><span>Current Time: </span>{0}</li>", DateTime.Now));
                        sb.Append(String.Format("<li><span>Last Visit: </span>{0}</li>", Model.AppCookies.LastVisit));
                        sb.Append(String.Format("<li><span>Preferences: </span>{0}</li>", Model.AppCookies.Preferences));
                        sb.Append(String.Format("<li><span>Security Key: </span>{0}</li>", Model.AppCookies.UserAccountSecurityKey));
                        sb.Append("</ul>");
                    }

                    // Current user
                    if (Model.CurrentUserAccount != null && Model.CurrentUserAccount.useraccountid > 0)
                    {
                        sb.Append("<h3>Current user</h3>");
                        sb.Append("<ul>");
                        sb.Append(String.Format("<li><span>User Account Id: </span>{0}</li>", Model.CurrentUserAccount.useraccountid));
                        sb.Append(String.Format("<li><span>User Group: </span>{0}</li>", Model.CurrentUserAccount.usergroup.name));
                        sb.Append(String.Format("<li><span>Username: </span>{0}</li>", Model.CurrentUserAccount.email));
                        sb.Append(String.Format("<li><span>Security Key Match: </span>{0}</li>", Model.CurrentUserAccount.securitykey == Model.AppCookies.UserAccountSecurityKey ? "True" : "False"));
                        sb.Append("</ul>");
                    }

                    // ADMIN SETTINGS
                    sb.Append("<h3>Admin Settings</h3>");
                    sb.Append("<ul>");
                    sb.Append(String.Format("<li><span>CKFinder Base Url: </span>{0}</li>", MotorMart.Core.Common.GlobalSettings.CKFinderBaseUrl));
                    sb.Append(String.Format("<li><span>CKFinder Base Directory: </span>{0}</li>", MotorMart.Core.Common.GlobalSettings.CKFinderBaseDir));
                    sb.Append(String.Format("<li><span>Client Resource Directory: </span>{0}</li>", MotorMart.Core.Common.GlobalSettings.ClientResourceDirectory));
                    sb.Append("</ul>");


                    if (Model.GetType() == typeof(VehicleViewModel))
                    {
                        VehicleViewModel vModel = (VehicleViewModel)Model;

                        if (vModel != null)
                        {
                            if (vModel.CurrentVehicle != null)
                            {
                                sb.Append("<h3>Vehicle data</h3>");
                                sb.Append("<ul>");
                                sb.Append(String.Format("<li><span>Vehicle Id: </span>{0}</li>", vModel.CurrentVehicle.vehicleid));
                                sb.Append(String.Format("<li><span>Product Ref: </span>{0}</li>", vModel.CurrentVehicle.reference));
                                sb.Append("</ul>");
                            }
                        }
                    }                    
                }

                sb.Append("</div>");

                sb.Append("<style type=\"text/css\">");
                sb.Append("#debug-window{	background-color:#fde099; color:#252525; font-size:12px;	border:1px solid #999999; cursor:pointer;	padding:10px; top:10px;	position:fixed;	right:10px; width:400px;	z-index:100;}");
                sb.Append("#debug-window ul{	margin:0;	list-style:none; padding-bottom:10px;}");
                sb.Append("#debug-window ul li { padding-bottom:2px;");
                sb.Append("#debug-window ul li span { font-weight:bold;}");
                sb.Append("</style>");
                sb.Append("<script type=\"text/javascript\">$(document).ready(function() { $('#debug-window').fadeTo('fast', 1); $('#debug-window').find('ul').slideToggle(); $('#debug-window').click(function() { $(this).find('ul').slideToggle(); });});</script>");
            }

            return sb.ToString();
        }

        internal static string Output(StringBuilder sb)
        {
            return sb.ToString();
        }

    }
}