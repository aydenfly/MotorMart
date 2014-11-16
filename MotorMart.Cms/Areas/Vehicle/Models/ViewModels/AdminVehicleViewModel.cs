using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Core.Models;
using MotorMart.Cms.Models;


namespace MotorMart.Cms.Areas.Vehicle.Models
{
    public class AdminVehicleViewModel : AdminViewModel
    {
        public vehicle CurrentVehicle { get; set; }
        public PagedList<vehicle> VehicleResults { get; set; }

        #region Select lists
        public SelectList MakeSelect { get; set; }
        public SelectList ModelSelect { get; set; }
        public SelectList FuelTypeSelect { get; set; }
        public SelectList BodyTypeSelect { get; set; }
        public SelectList TransmissionSelect { get; set; }
        public SelectList ColorSelect { get; set; }
        public SelectList DoorsSelect { get; set; }
        public SelectList DealerSelect { get; set; }
        #endregion

        #region Essentials
        public VehicleGetModel get { get; set; }
        public VehicleDetailsAddModel adddetails { get; set; }
        public VehicleDetailsEditModel editdetails { get; set; }
        public VehicleDeleteModel delete { get; set; }
        #endregion
        
        #region Extras
        public VehicleSummaryDetailsEditModel editsummarydetails { get; set; }
        public VehicleDimensionsEditModel editdimensions { get; set; }
        public VehicleFeaturesEditModel editfeatures { get; set; }
        public VehiclePerformanceEditModel editperformance { get; set; }
        public VehicleSafetyDetailsEditModel editsafetydetails { get; set; }
        #endregion
        
        #region Images
        public VehicleImageAddModel vehicleimageadd { get; set; }
        public VehicleImageEditModel vehicleimageedit { get; set; }
        public VehicleImageDeleteModel vehicleimagedelete { get; set; }

        public string[] ImageDimensions;
        #endregion

        public bool Success { get; set; }       
    }
}