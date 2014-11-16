using System.Web.Mvc;

namespace MotorMart.Cms.Areas.Misc
{
    public class MiscAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Misc";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            string Namespace = "MotorMart.Cms.Areas.Misc.Controllers";

            #region Misc routes

            #region Application Settings

            context.MapRoute(
                "ApplicationSettingDeleteDialog",
                "misc/applicationsetting/deletedialog/{applicationsettingid}",
                new { controller = "ApplicationSetting", action = "DeleteDialog", applicationsettingid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "ApplicationSettingDelete",
                "misc/applicationsetting/edit/{applicationsettingid}/delete",
                new { controller = "ApplicationSetting", action = "Delete", applicationsettingid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "ApplicationSettingAdd",
                "misc/applicationsetting/add",
                new { controller = "ApplicationSetting", action = "Add" },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "ApplicationSettingEdit",
                "misc/applicationsetting/edit/{applicationsettingid}",
                new { controller = "ApplicationSetting", action = "Edit", applicationsettingid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            

            context.MapRoute(
                "ApplicationSetting_default",
                "misc/applicationsetting/{page}",
                new { area = "Misc", controller = "ApplicationSetting", action = "Index", page = UrlParameter.Optional },
                null,
                new string[] { Namespace }
            );

            #endregion

            #region Vehicle Dealer

            context.MapRoute(
                "DealerDeleteDialog",
                "misc/dealer/deletedialog/{dealerid}",
                new { controller = "Dealer", action = "DeleteDealerDialog", dealerid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "DealerDelete",
                "misc/dealer/edit/{dealerid}/delete",
                new { controller = "Dealer", action = "DeleteDealer", dealerid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "DealerAdd",
                "misc/dealer/add",
                new { controller = "Dealer", action = "AddDealer" },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "DealerEdit",
                "misc/dealer/edit/{dealerid}",
                new { controller = "Dealer", action = "EditDealer", dealerid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "DealerMoveUp",
                "misc/dealer/edit/{dealerid}/up",
                new { controller = "Dealer", action = "Up", dealerid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "DealerMoveDown",
                "misc/dealer/edit/{dealerid}/down",
                new { controller = "Dealer", action = "Down", dealerid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "Dealer_default",
                "misc/dealer/{page}",
                new { area = "Misc", controller = "Dealer", action = "Index", page = UrlParameter.Optional },
                null,
                new string[] { Namespace }
            );

            #endregion
    
            #region Vehicle Make

            context.MapRoute(
                "VehicleMakeDeleteDialog",
                "misc/vehiclemake/deletedialog/{makeid}",
                new { controller = "VehicleMake", action = "DeleteVehicleMakeDialog", makeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "VehicleMakeDelete",
                "misc/vehiclemake/edit/{makeid}/delete",
                new { controller = "VehicleMake", action = "DeleteVehicleMake", makeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "VehicleMakeAdd",
                "misc/vehiclemake/add",
                new { controller = "VehicleMake", action = "AddVehicleMake" },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "VehicleMakeEdit",
                "misc/vehiclemake/edit/{makeid}",
                new { controller = "VehicleMake", action = "EditVehicleMake", makeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "VehicleMakeMoveUp",
                "misc/vehiclemake/edit/{makeid}/up",
                new { controller = "VehicleMake", action = "Up", makeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "VehicleMakeMoveDown",
                "misc/vehiclemake/edit/{makeid}/down",
                new { controller = "VehicleMake", action = "Down", makeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "VehicleMake_default",
                "misc/vehiclemake/{page}",
                new { area = "Misc", controller = "VehicleMake", action = "Index", page = UrlParameter.Optional },
                null,
                new string[] { Namespace }
            );

            #endregion

            #region Vehicle Models

            context.MapRoute(
                "VehicleModelDeleteDialog",
                "misc/vehiclemodel/deletedialog/{modelid}",
                new { controller = "VehicleModel", action = "DeleteVehicleModelDialog", modelid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "VehicleModelDelete",
                "misc/vehiclemodel/edit/{modelid}/delete",
                new { controller = "VehicleModel", action = "DeleteVehicleModel", modelid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "VehicleModelAdd",
                "misc/vehiclemodel/add",
                new { controller = "VehicleModel", action = "AddVehicleModel" },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "VehicleModelEdit",
                "misc/vehiclemodel/edit/{modelid}",
                new { controller = "VehicleModel", action = "EditVehicleModel", modelid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "VehicleModelMoveUp",
                "misc/vehiclemodel/{modelid}/up",
                new { controller = "VehicleModel", action = "Up", modelid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "VehicleModelMoveDown",
                "misc/vehiclemodel/{modelid}/down",
                new { controller = "VehicleModel", action = "Down", modelid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "VehicleModel_default",
                "misc/vehiclemodel",
                new { area = "Misc", controller = "VehicleModel", action = "Index" },
                null,
                new string[] { Namespace }
            );
            
            #endregion

            #region Fuel type

            context.MapRoute(
                "FuelTypeDeleteDialog",
                "misc/fueltype/deletedialog/{fueltypeid}",
                new { controller = "FuelType", action = "DeleteFuelTypeDialog", fueltypeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "FuelTypeDelete",
                "misc/fueltype/edit/{fueltypeid}/delete",
                new { controller = "FuelType", action = "DeleteFuelType", fueltypeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "FuelTypeAdd",
                "misc/fueltype/add",
                new { controller = "FuelType", action = "AddFuelType" },
                null,
                new string[] { Namespace }

                );
            
            context.MapRoute(
                "FuelTypeEdit",
                "misc/fueltype/edit/{fueltypeid}",
                new { controller = "FuelType", action = "EditFuelType", fueltypeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "FuelTypeMoveUp",
                "misc/fueltype/edit/{fueltypeid}/up",
                new { controller = "FuelType", action = "Up", fueltypeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "FuelTypeMoveDown",
                "misc/fueltype/edit/{fueltypeid}/down",
                new { controller = "FuelType", action = "Down", fueltypeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "FuelType_default",
                "misc/fueltype",
                new { area = "Misc", controller = "FuelType", action = "Index" },
                null,
                new string[] { Namespace }
            );
            
            #endregion

            #region Body type

            context.MapRoute(
                "BodyTypeDeleteDialog",
                "misc/bodytype/deletedialog/{bodytypeid}",
                new { controller = "BodyType", action = "DeleteBodyTypeDialog", bodytypeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "BodyTypeDelete",
                "misc/bodytype/edit/{bodytypeid}/delete",
                new { controller = "BodyType", action = "DeleteBodyType", bodytypeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "BodyTypeAdd",
                "misc/bodytype/add",
                new { controller = "BodyType", action = "AddBodyType" },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "BodyTypeEdit",
                "misc/bodytype/edit/{bodytypeid}",
                new { controller = "BodyType", action = "EditBodyType", bodytypeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "BodyTypeMoveUp",
                "misc/bodytype/edit/{bodytypeid}/up",
                new { controller = "BodyType", action = "Up", bodytypeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "BodyTypeMoveDown",
                "misc/bodytype/edit/{bodytypeid}/down",
                new { controller = "BodyType", action = "Down", bodytypeid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "BodyType_default",
                "misc/bodytype",
                new { area = "Misc", controller = "BodyType", action = "Index" },
                null,
                new string[] { Namespace }
            );
           
            #endregion

            #region Transmission

            context.MapRoute(
                "TransmissionDeleteDialog",
                "misc/transmission/deletedialog/{transmissionid}",
                new { controller = "Transmission", action = "DeleteTransmissionDialog", transmissionid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "TransmissionDelete",
                "misc/transmission/edit/{transmissionid}/delete",
                new { controller = "Transmission", action = "DeleteTransmission", transmissionid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "TransmissionAdd",
                "misc/transmission/add",
                new { controller = "Transmission", action = "AddTransmission" },
                null,
                new string[] { Namespace }
                );
            
            context.MapRoute(
                "TransmissionEdit",
                "misc/transmission/edit/{transmissionid}",
                new { controller = "Transmission", action = "EditTransmission", transmissionid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "TransmissionMoveUp",
                "misc/transmission/edit/{transmissionid}/up",
                new { controller = "Transmission", action = "Up", transmissionid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "TransmissionMoveDown",
                "misc/transmission/edit/{transmissionid}/down",
                new { controller = "Transmission", action = "Down", transmissionid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "Transmission_default",
                "misc/transmission",
                new { area = "Misc", controller = "Transmission", action = "Index" },
                null,
                new string[] { Namespace }
            );
            
            #endregion

            #region Vehicle color

            context.MapRoute(
                "VehicleColorDeleteDialog",
                "misc/vehiclecolor/deletedialog/{colorid}",
                new { controller = "VehicleColor", action = "DeleteVehicleColorDialog", colorid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "VehicleColorDelete",
                "misc/vehiclecolor/edit/{colorid}/delete",
                new { controller = "VehicleColor", action = "DeleteVehicleColor", colorid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "VehicleColorAdd",
                "misc/vehiclecolor/add",
                new { controller = "VehicleColor", action = "AddVehicleColor" },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "VehicleColorEdit",
                "misc/vehiclecolor/edit/{colorid}",
                new { controller = "VehicleColor", action = "EditVehicleColor", colorid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "VehicleColorMoveUp",
                "misc/vehiclecolor/edit/{colorid}/up",
                new { controller = "VehicleColor", action = "Up", colorid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );
            context.MapRoute(
                "VehicleColorMoveDown",
                "misc/vehiclecolor/edit/{colorid}/down",
                new { controller = "VehicleColor", action = "Down", colorid = UrlParameter.Optional },
                null,
                new string[] { Namespace }
                );

            context.MapRoute(
                "VehicleColor_default",
                "misc/vehiclecolor",
                new { area = "Misc", controller = "VehicleColor", action = "Index" },
                null,
                new string[] { Namespace }
            );
            
            #endregion

            context.MapRoute(
                "Misc_default",
                "misc/{controller}/{action}/{id}",
                new { area = "Misc", controller = "Misc", action = "Index", id = UrlParameter.Optional },
                null,
                new string[] { Namespace }
            );

            #endregion
        }
    }
}
