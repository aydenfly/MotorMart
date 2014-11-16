using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using MotorMart.Core.Models;

namespace MotorMart.Core.Common
{
    public static class GlobalSettings
    {
        private static LinqApplicationSettingRepository _repository = new LinqApplicationSettingRepository();
        
        private static string dalConnectionString;
        public static string DALConnectionString
        {
            get
            {

                dalConnectionString = WebConfigurationManager.ConnectionStrings["MotorMartConnectionString"].ConnectionString;                
                return dalConnectionString;
            }
        }

        private static string applicationVersion;
        public static string ApplicationVersion
        {
            get
            {
                applicationVersion = WebConfigurationManager.AppSettings["ApplicationVersion"].ToString();
                return applicationVersion;
            }
        }

        private static string clientName;
        public static string ClientName
        {            
            get
            {   
                clientName = _repository.GetApplicationSetting("ClientName");
                return clientName;
            }
        }

        private static string clientSiteUrl;
        public static string ClientSiteUrl
        {
            get
            {
                clientSiteUrl = _repository.GetApplicationSetting("ClientSiteUrl");
                return clientSiteUrl;
            }
        }

        private static string clientSiteBaseDir;
        public static string ClientSiteBaseDir
        {
            get
            {
                if (WebConfigurationManager.AppSettings["ClientSiteBaseDir"] != null)
                {
                    clientSiteBaseDir = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ClientSiteBaseDir"].ToString()) + "/";
                }
                else
                {
                    clientSiteBaseDir = "[Not Set]";
                }
                return clientSiteBaseDir;
            }
        }

        private static string cKFinderBaseDir;
        public static string CKFinderBaseDir
        {
            get
            {
                if (WebConfigurationManager.AppSettings["CKFinderBaseDir"] != null)
                {
                    cKFinderBaseDir = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["CKFinderBaseDir"].ToString()) + "/";
                }
                else
                {
                    cKFinderBaseDir = "[Not Set]";
                }
                return cKFinderBaseDir;
            }
        }

        private static string cKFinderBaseUrl;
        public static string CKFinderBaseUrl
        {
            get
            {
                if (WebConfigurationManager.AppSettings["CKFinderBaseDir"] != null)
                {
                    cKFinderBaseUrl = WebConfigurationManager.AppSettings["CKFinderBaseDir"].ToString() + "/";
                }
                return cKFinderBaseUrl;
            }
        }

        private static string webRoot;
        public static string WebRoot
        {
            get
            {
                if (webRoot == null)
                {
                   
                    string Port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
                    if (Port == null || Port == "80" || Port == "443") Port = "";
                    else Port = ":" + Port;

                    string Protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];

                    if (Protocol == null || Protocol == "0") Protocol = "http://";
                    else Protocol = "https://";

                    string applicationPath = (HttpContext.Current.Request.ApplicationPath.EndsWith("/")) ? HttpContext.Current.Request.ApplicationPath : "/";

                    webRoot = Protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + Port + applicationPath;
                    webRoot = BuildBaseUri(webRoot);
        
                }
                return webRoot;
            }
        }

        private static string clientResourceDirectory;
        public static string ClientResourceDirectory
        {
            get
            {
                if (WebConfigurationManager.AppSettings["ClientResourceDirectory"] != null)
                    clientResourceDirectory = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ClientResourceDirectory"].ToString()) + "/";
                else
                    clientResourceDirectory = HttpContext.Current.Server.MapPath("/res/");

                return clientResourceDirectory;
            }
        }

        private static string BuildBaseUri(string url)
        {            
            url = url.EndsWith("/") ? url.Substring(0, url.Length - 1) : url;
            return url;
        }

        private static string cookieDomain;
        public static string CookieDomain
        {
            get
            {
                cookieDomain = _repository.GetApplicationSetting("CookieDomain");
                return cookieDomain;
            }
        }

        private static string adminSiteUrl;
        public static string AdminSiteUrl
        {
            get
            {
                adminSiteUrl = _repository.GetApplicationSetting("AdminSiteUrl");
                return adminSiteUrl;
            }
        }

        private static string adminLoginLogo;
        public static string AdminLoginLogo
        {
            get
            {
                adminLoginLogo = _repository.GetApplicationSetting("AdminLoginLogo");
                return adminLoginLogo;
            }
        }

        private static string vehicleImageDirectory;
        public static string VehicleImageDirectory
        {
            get
            {
                vehicleImageDirectory = _repository.GetApplicationSetting("VehicleImageDirectory");
                return vehicleImageDirectory;
            }
        }

        private static string vehicleImageDimensions;
        public static string VehicleImageDimensions
        {
            get
            {
                vehicleImageDimensions = _repository.GetApplicationSetting("VehicleImageDimensions");
                return vehicleImageDimensions;
            }
        }

        private static string vehicleMakeLogoDirectory;
        public static string VehicleMakeLogoDirectory
        {
            get
            {
                vehicleMakeLogoDirectory = _repository.GetApplicationSetting("VehicleMakeLogoDirectory");
                return vehicleMakeLogoDirectory;
            }
        }

        private static string vehicleMakeLogoDimensions;
        public static string VehicleMakeLogoDimensions
        {
            get
            {
                vehicleMakeLogoDimensions = _repository.GetApplicationSetting("VehicleMakeLogoDimensions");
                return vehicleMakeLogoDimensions;
            }
        }

        private static string vehicleDealerLogoDirectory;
        public static string VehicleDealerLogoDirectory
        {
            get
            {
                vehicleDealerLogoDirectory = _repository.GetApplicationSetting("VehicleDealerLogoDirectory");
                return vehicleDealerLogoDirectory;
            }
        }

        private static string vehicleDealerLogoDimensions;
        public static string VehicleDealerLogoDimensions
        {
            get
            {
                vehicleDealerLogoDimensions = _repository.GetApplicationSetting("VehicleDealerLogoDimensions");
                return vehicleDealerLogoDimensions;
            }
        }

        private static string dateFormat;
        public static string DateFormat
        {
            get
            {
                dateFormat = _repository.GetApplicationSetting("DateFormat");
                return dateFormat;
            }
        }

        private static bool sendEmailOnError;
        public static bool SendEmailOnError
        {
            get
            {   
                sendEmailOnError = (_repository.GetApplicationSetting("SendEmailOnError") == "1");
                return sendEmailOnError;
            }
        }

        private static bool writetoErrorLog;
        public static bool WritetoErrorLog
        {
            get
            {
                writetoErrorLog = (_repository.GetApplicationSetting("WriteToErrorLog") == "1");
                return writetoErrorLog;
            }
        }

        private static string errorEmailAddress;
        public static string ErrorEmailAddress
        {
            get
            {
                errorEmailAddress = _repository.GetApplicationSetting("ErrorEmailAddress");
                return errorEmailAddress;
            }
        }

        private static string errorLogFile;
        public static string ErrorLogFile
        {
            get
            {
                errorLogFile = HttpContext.Current.Server.MapPath(_repository.GetApplicationSetting("ErrorLogFile"));
                return errorLogFile;
            }
        }

        private static string rebuildRoutesUrl;
        public static string RebuildRoutesUrl
        {
            get
            {
                rebuildRoutesUrl = _repository.GetApplicationSetting("RebuildRoutesUrl");
                return rebuildRoutesUrl;
            }
        }

        private static bool updateRoutes;
        public static bool UpdateRoutes
        {
            get
            {
                updateRoutes = WebConfigurationManager.AppSettings["UpdateRoutes"].ToString() == "1";
                return updateRoutes;
            }
        }
    }
}
