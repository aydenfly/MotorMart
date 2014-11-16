using System.Web.Mvc;

namespace MotorMart.Cms.Areas.Vehicle
{
    public class VehicleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Vehicle";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            string Namespace = "MotorMart.Cms.Areas.Vehicle.Controllers";
            
            #region Vehicle

            #region Images

            context.MapRoute(
                "VehicleImageAdd",
                "vehicle/edit/{vehicleid}/images/add",
                new { controller = "Vehicle", action = "AddImage", vehicleid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
               "VehicleImageEdit",
               "vehicle/edit/{vehicleid}/images/edit",
               new { controller = "Vehicle", action = "EditImages", vehicleid = UrlParameter.Optional },
               null,
               new string[] { Namespace }
               );
            context.MapRoute(
               "VehicleImageDelete",
               "vehicle/edit/{vehicleid}/images/delete",
               new { controller = "Vehicle", action = "DeleteImage", vehicleid = UrlParameter.Optional },
               null,
               new string[] { Namespace }
               );

            #endregion

            context.MapRoute(
                "VehicleDeleteDialog",
                "vehicle/deletedialog/{vehicleid}",
                new { controller = "Vehicle", action = "VehicleDeleteDialog", sitemapid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            
            context.MapRoute(
                "VehicleDelete",
                "vehicle/edit/{vehicleid}/delete",
                new { controller = "Vehicle", action = "Delete", vehicleid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "VehicleEdit",
                "vehicle/edit/{vehicleid}",
                new { controller = "Vehicle", action = "Edit", vehicleid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "VehicleSafetyDetailsEdit",
                "vehicle/edit/{vehicleid}/safety-details",
                new { controller = "Vehicle", action = "EditSafetyDetails", vehicleid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "VehicleFeaturesEdit",
                "vehicle/edit/{vehicleid}/features",
                new { controller = "Vehicle", action = "EditVehicleFeatures", vehicleid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "VehiclePerformanceDetailsEdit",
                "vehicle/edit/{vehicleid}/performance",
                new { controller = "Vehicle", action = "EditPerformanceDetails", vehicleid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "VehicleDimensionsEdit",
                "vehicle/edit/{vehicleid}/dimensions",
                new { controller = "Vehicle", action = "EditVehicleDimensions", vehicleid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "VehicleSummaryDetailsEdit",
                "vehicle/edit/{vehicleid}/summarydetails",
                new { controller = "Vehicle", action = "EditVehicleSummaryDetails", vehicleid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "VehicleAdd",
                "vehicle/add",
                new { controller = "Vehicle", action = "Add" },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "Vehicle_default",
                "vehicle/{controller}/{action}/{id}",
                new { controller = "Vehicle", action = "Search", id = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            #endregion           
        }
    }
}
