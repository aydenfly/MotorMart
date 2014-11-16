using System.Web.Mvc;

namespace MotorMart.Cms.Areas.Account
{
    public class AccountAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Account";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            string Namespace = "MotorMart.Cms.Areas.Account.Controllers";

            context.MapRoute(
                "Account_Login",
                "login",
                new { area = "account", controller = "login", action = "index" },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "Account_Logout",
                "logout",
                new { area = "account", controller = "login", action = "logout" },
                null,
                new string[] { Namespace }
                );

            //context.MapRoute(
            //    "Account_default",
            //    "account/{controller}/{action}/{id}",
            //    new { area = "account", controller = "login", action = "index", id = UrlParameter.Optional },
            //    null,
            //    new string[] { Namespace }
            //);
        }
    }
}
