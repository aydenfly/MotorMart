using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Core.Models;

namespace MotorMart.Web.Models
{
    public class VehicleSearchViewModel : MasterViewModel
    {        
        public VehicleSearchModel vehiclesearch { get; set; }

        public PagedList<VehicleSearchResult> SearchResults { get; set; }

        public class VehicleSearchResult
        {
            public int VehicleId;
            public int YearReg;
            public string MainPhoto;
            public string Name;
            public string DealerName;
            public string DealerLogo;
            public string ShortDescription;
            public decimal Price;
            public string Transmission;
            public string EngineSize;
            public int Mileage;
            public string FuelType;
            public string Distance;
        }
    }
}