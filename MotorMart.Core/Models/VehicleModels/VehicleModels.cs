using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MotorMart.Core.Models;
using MotorMart.Core.Models.Validation.DataAnnotations;

namespace MotorMart.Core.Models
{
    public class VehicleGetModel
    {
        public int vehicleid { get; set; }
    }

    public class VehicleDetailsAddModel
    {
        public vehicle NewVehicle;

        [Required(ErrorMessage = "Reference is required")]
        [DisplayName("Vehicle reference")]
        public string reference { get; set; }

        [Required(ErrorMessage = "Vehicle name is required")]
        [DisplayName("Vehicle name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Vehicle make is required")]
        [DisplayName("Vehicle make")]
        public int makeid { get; set; }

        [Required(ErrorMessage = "Vehicle model is required")]
        [DisplayName("Model")]
        public int modelid { get; set; }

        [DisplayName("Transmission")]
        public int transmisionid { get; set; }

        [Required(ErrorMessage = "Fuel type is required")]
        [DisplayName("Fuel type")]
        public int fueltypeid { get; set; }

        [Required(ErrorMessage = "Body type is required")]
        [DisplayName("Body type")]
        public int bodytypeid { get; set; }

        [Required(ErrorMessage = "Vehicle color is required")]
        [DisplayName("Vehicle color")]
        public int colorid { get; set; }

        [Required(ErrorMessage = "Vehicle dealer is required")]
        [DisplayName("Vehicle dealer")]
        public int dealerid { get; set; }

        [Required(ErrorMessage = "Please add a brief description")]
        [DisplayName("Short description")]
        [StringLength(250, ErrorMessage = "Short description cannot exceed 250 characters.")]
        public string shortdescription { get; set; }

        [DisplayName("Full description")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string fulldescription { get; set; }

        [DisplayName("Is new")]
        public bool isnew { get; set; }

        [DisplayName("Enabled")]
        public bool enabled { get; set; }

    }

    public class VehicleDetailsEditModel
    {
        public int vehicleid { get; set; }

        [Required(ErrorMessage = "Reference is required")]
        [DisplayName("Vehicle reference")]
        public string reference { get; set; }

        [Required(ErrorMessage = "Vehicle name is required")]
        [DisplayName("Vehicle name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Vehicle make is required")]
        [DisplayName("Vehicle make")]
        public int makeid { get; set; }

        [Required(ErrorMessage = "Vehicle model is required")]
        [DisplayName("Model")]
        public int modelid { get; set; }

        [DisplayName("Transmission")]
        public int transmisionid { get; set; }

        [Required(ErrorMessage = "Fuel type is required")]
        [DisplayName("Fuel type")]
        public int fueltypeid { get; set; }

        [Required(ErrorMessage = "Body type is required")]
        [DisplayName("Body type")]
        public int bodytypeid { get; set; }

        [Required(ErrorMessage = "Vehicle color is required")]
        [DisplayName("Vehicle color")]
        public int colorid { get; set; }

        [Required(ErrorMessage = "Vehicle dealer is required")]
        [DisplayName("Vehicle dealer")]
        public int dealerid { get; set; }

        [Required(ErrorMessage = "Please add a brief description")]
        [DisplayName("Short description")]
        public string shortdescription { get; set; }

        [DisplayName("Full description")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string fulldescription { get; set; }

        [DisplayName("Is new")]
        public bool isnew { get; set; }

        [DisplayName("Enabled")]
        public bool enabled { get; set; }

    }

    public class VehicleSummaryDetailsEditModel
    {
        public int vehicleid { get; set; }

        [Required(ErrorMessage = "How many doors does this vehicle have?")]
        [DisplayName("Number of doors")]
        [Integer(ErrorMessage = "Please supply a valid doors value")]
        public int numberofdoors { get; set; }

        [Required(ErrorMessage = "How many seats does this vehicle have?")]
        [DisplayName("Number of seats")]
        [Integer(ErrorMessage = "Please supply a valid seats value")]
        public int numberofseats { get; set; }

        [Required(ErrorMessage = "Mileage is required")]
        [DisplayName("Mileage")]
        [Integer(ErrorMessage = "Please supply a valid mileage value")]
        public int mileage { get; set; }

        [Required(ErrorMessage = "A valid date of manufacture is required")]
        [DisplayName("Date of Manufacture")]
        [IsDateTime]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dateofmanufacture { get; set; }

        [DisplayName("Engine size")]
        public decimal enginesize { get; set; }

        [DisplayName("CO2 emissions")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string co2emissions { get; set; }

        [DisplayName("Manufacturer warranty years")]
        [Integer(ErrorMessage = "Please supply a valid year value")]
        public int manufacturerwarrantyyears { get; set; }

        [DisplayName("Manufacturer warranty miles")]
        [Integer(ErrorMessage = "Please supply a valid miles value")]
        public int manufacturerwarrantymiles { get; set; }

        [DisplayName("Paint work guarantee years")]
        [Integer(ErrorMessage = "Please supply a valid year value")]
        public int paintworkguaranteeyears { get; set; }

        [DisplayName("Corrosion guarantee years")]
        [Integer(ErrorMessage = "Please supply a valid year value")]
        public int corrosionguaranteeyears { get; set; }

        [DisplayName("Tax band")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string taxband { get; set; }

        [Required(ErrorMessage = "Year of Registration is required")]
        [DisplayName("Year of registration")]
        [Integer(ErrorMessage = "Please supply a valid year of registration")]
        public int yearofregistration { get; set; }

        [DisplayName("Date of Sale")]
        [IsDateTime]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dateofsale { get; set; }

        [Required(ErrorMessage = "What is your selling price?")]
        [DisplayName("Selling price")]
        public decimal? sellingprice { get; set; }
    }

    public class VehicleDeleteModel
    {
        public vehicle CurrentVehicle;
        public int vehicleid { get; set; }
    }

    public class VehicleDimensionsEditModel
    {
        public int vehicleid { get; set; }
        public int dimensionsid { get; set; }

        [DisplayName("height")]
        public decimal? height { get; set; }

        [DisplayName("Wheel base")]
        public decimal? wheelbase { get; set; }

        [DisplayName("Width")]
        public decimal? width { get; set; }

        [DisplayName("Width including mirrors")]
        public decimal? widthincludingmirrors { get; set; }

        [DisplayName("Fuel tank capacity")]
        public decimal? fueltankcapacity { get; set; }

        [DisplayName("Gross vehicle weight")]
        public decimal? grossvehicleweight { get; set; }

        [DisplayName("Luggage capacity with seats down")]
        public decimal? luggagecapacitywithseatsdown { get; set; }

        [DisplayName("Luggage capacity with seats up")]
        public decimal? luggagecapacitywithseatsup { get; set; }

        [DisplayName("Max loading weight")]
        public decimal? maxloadingweight { get; set; }

        [DisplayName("Max. roof load")]
        public decimal? maxroofload { get; set; }

        [DisplayName("Max. towing weight braked")]
        public decimal? maxtowingweightbraked { get; set; }

        [DisplayName("Max. towing weight unbraked")]
        public decimal? maxtowingweightunbraked { get; set; }

        [DisplayName("Min. kerb weight")]
        public decimal? minkerbweight { get; set; }

        [DisplayName("Kerb to kerb turning circle")]
        public decimal? kerbtokerbturningcircle { get; set; }
    }   

    public class VehicleFeaturesEditModel
    {
        public int vehicleid { get; set; }
        
        public int featuresid { get; set; }

        [DisplayName("Interior details")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string interiordetails { get; set; }

        [DisplayName("Exterior details")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string exteriordetails { get; set; }
    }

    public class VehiclePerformanceEditModel
    {
        public int vehicleid { get; set; }

        public int performanceid { get; set; }

        [DisplayName("Urban fuel consumption")]
        public decimal? urbanfuelconsumption { get; set; }

        [DisplayName("Extra urban fuel consumption")]
        public decimal? extraurbanfuelconsumption { get; set; }

        [DisplayName("Combined fuel consumption")]
        public decimal? combinedfuelconsumption { get; set; }

        [DisplayName("Acceleration")]
        public decimal? acceleration { get; set; }

        [DisplayName("Top speed")]
        public int? topspeed { get; set; }

        [DisplayName("Cylinders")]
        public int? cylinders { get; set; }

        [DisplayName("Valves")]
        public int? valves { get; set; }

        [DisplayName("Engine power")]
        public int? enginepower { get; set; }

        [DisplayName("Engine torque")]
        public int? enginetorque { get; set; }
        
    }

    public class VehicleSafetyDetailsEditModel
    {
        public int vehicleid { get; set; }

        public int safetydetailsid { get; set; }

        [DisplayName("Safety details")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string details { get; set; }
    }
    
    public class VehicleImageAddModel
    {
        public int vehicleid { get; set; }

        [DisplayName("Select image...")]
        public HttpPostedFileBase fileinput { get; set; }

        [DisplayName("Caption")]
        [StringLength(250)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string caption { get; set; }
    }

    public class VehicleImageDeleteModel
    {
        public int vehicleid { get; set; }
        public int vehicleimageid { get; set; }
    }

    public class VehicleImageEditModel
    {
        public int vehicleid { get; set; }
        public List<VehicleImageItem> items { get; set; }
    }

    public class VehicleImageItem
    {
        public string filename { get; set; }
        
        public HttpPostedFileBase fileinput;

        public int vehicleimageid { get; set; }

        [DisplayName("Caption")]
        [StringLength(250)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string caption { get; set; }

        [DisplayName("Sort order")]
        public int sortorder { get; set; }
    }

    public class AdminVehicleSearchModel
    {
        public SelectList MakeSelect;
        public SelectList ModelSelect;
        public SelectList FuelTypeSelect;
        public SelectList AgeSelect;
        public SelectList MileageSelect;
        public SelectList DistanceSelect;
        public SelectList EngineSizeSelect;
        public SelectList BodyTypeSelect;
        public SelectList TransmissionSelect;
        public SelectList ColorSelect;
        public SelectList DoorsSelect;
        public SelectList MinPriceSelect;
        public SelectList MaxPriceSelect;
        public SelectList SortBySelect;

        [DisplayName("Deal distance")]
        public int? dealerdistance { get; set; }

        [DisplayName("Vehicle make")]
        public int? makeid { get; set; }

        [DisplayName("Fuel type")]
        public int? fueltypeid { get; set; }

        [DisplayName("Model")]
        public int? modelid { get; set; }

        [DisplayName("Body type")]
        public int? bodytypeid { get; set; }

        [DisplayName("Transmission")]
        public int? transmissionid { get; set; }

        [DisplayName("Color")]
        public int? colorid { get; set; }

        [DisplayName("Engine size")]
        public int? enginesize { get; set; }

        [DisplayName("Vehicle Age")]
        public int? vehicleage { get; set; }

        [DisplayName("Mileage")]
        public int? vehiclemileage { get; set; }

        [DisplayName("Min. Price")]
        public int? minprice { get; set; }

        [DisplayName("Max. Price")]
        public int? maxprice { get; set; }

        [DisplayName("No. of doors")]
        public int? numberofdoors { get; set; }

        [DisplayName("Keyword(s)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string keywords { get; set; }

        public DateTime? DateFrom;
        public DateTime? DateTo;

        [DisplayName("Date from")]
        public string datefrom { get; set; }

        [DisplayName("Date to")]
        public string dateto { get; set; }

        [DisplayName("Sort by")]
        public int sortby { get; set; }

        public int page { get; set; }      

    }

    public class AdminVehicleSearchResult
    {
        public int VehicleId;
        public string Name;
        public string DealerName;
        public string VehicleReg;
        public string Transmission;
        public string Mileage;
        public string Age;
        public string FuelType;
        public string Price;
        public string Enabled;
        public int SortOrder;
    }

    #region Front End Search Models

    public class VehicleSearchModel
    {
        public SelectList MakeSelect;
        public SelectList ModelSelect;
        public SelectList FuelTypeSelect;
        public SelectList AgeSelect;
        public SelectList MileageSelect;
        public SelectList DistanceSelect;
        public SelectList EngineSizeSelect;
        public SelectList BodyTypeSelect;
        public SelectList TransmissionSelect;
        public SelectList ColorSelect;
        public SelectList DoorsSelect;
        public SelectList MinPriceSelect;
        public SelectList MaxPriceSelect;
        public SelectList SortBySelect;

        [DisplayName("Deal distance")]
        public int? dealerdistance { get; set; }

        [DisplayName("Vehicle make")]
        public int? makeid { get; set; }

        [DisplayName("Fuel type")]
        public int? fueltypeid { get; set; }

        [DisplayName("Model")]
        public int? modelid { get; set; }

        [DisplayName("Body type")]
        public int? bodytypeid { get; set; }

        [DisplayName("Transmission")]
        public int? transmissionid { get; set; }

        [DisplayName("Color")]
        public int? colorid { get; set; }

        [DisplayName("Engine size")]
        public int? enginesize { get; set; }

        [DisplayName("Vehicle Age")]
        public int? vehicleage { get; set; }

        [DisplayName("Mileage")]
        public int? vehiclemileage { get; set; }

        [DisplayName("Min. Price")]
        public int? minprice { get; set; }

        [DisplayName("Max. Price")]
        public int? maxprice { get; set; }

        [DisplayName("No. of doors")]
        public int? numberofdoors { get; set; }

        [DisplayName("Post code")]
        [Required(ErrorMessage= "Please supply your postcode")]
        [PostCode(ErrorMessage="Enter a valid postcode")]
        public string postcode { get; set; }

        public DateTime? DateFrom;
        public DateTime? DateTo;

        [DisplayName("Date from")]
        public string datefrom { get; set; }

        [DisplayName("Date to")]
        public string dateto { get; set; }

        [DisplayName("Sort by")]
        public int sortby { get; set; }

        public bool isnew { get; set; }

        public int page { get; set; }

        public double visitorlat { get; set; }
        public double visitorlng { get; set; }

    }

    #endregion

}