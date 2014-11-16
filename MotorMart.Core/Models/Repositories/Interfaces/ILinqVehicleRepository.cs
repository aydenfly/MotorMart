using System;
using MotorMart.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace MotorMart.Core.Models
{
    public interface ILinqVehicleRepository
    {
        void AddVehicle(vehicle vehicleToAdd);

        void AddVehicleDealerDetails(dealer detailsToAdd);

        IQueryable<vehicle> AdminSearchVehicles(AdminVehicleSearchModel model);

        void DeleteVehicle(vehicle Vehicle);

        void DeleteVehicleImage(vehicleimage Image);

        IList<bodytype> GetBodyTypeList();

        IList<color> GetColorList();

        IList<dealer> GetDealerList();

        IList<fueltype> GetFuelTypeList();

        IList<transmission> GetTransmissionList();

        vehicle GetVehicle(int vehicleId);

        vehicle GetVehicleAbove(int vehicleId);

        vehicle GetVehicleBelow(int vehicleId);

        dealer GetVehicleDealerDetails(int DealerId);

        geolookup GetGeoLookUpByPostalCode(string postalcode);

        geolookup GetGeoLookUpByPostalCodeByCountry(string postalcode, string countrycode);

        void AddGeoLookUp(geolookup lookupToAdd);

        vehicleimage GetVehicleImage(int VehicleImageId);

        IList<make> GetVehicleMakeList();

        IList<model> GetVehicleModelList();

        IList<vehicleimage> GetVehicleImages(int VehicleId);

        IList<vehicle> ListVehicles();

        IQueryable<vehicle> SearchVehicles(VehicleSearchModel model);

        void Update();
    }
}
