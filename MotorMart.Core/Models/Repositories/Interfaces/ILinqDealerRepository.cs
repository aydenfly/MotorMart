using System;
using MotorMart.Core.Models;
using System.Collections.Generic;

namespace MotorMart.Core.Models
{
    public interface ILinqDealerRepository
    {
        void AddDealer(dealer DealerToAdd);

        bool DealerExists(string name);

        void DeleteDealer(dealer Dealer);

        dealer GetDealer(int DealerId);

        dealer GetDealer(string name);

        dealer GetDealerAbove(int DealerId);

        dealer GetDealerBelow(int DealerId);

        IList<dealer> GetDealers();

        IList<dealer> GetDealers(int DealerId);

        IList<country> GetCountriesList();

        country GetCountryById(int countryId);

        geolookup GetGeoLookUpByPostalCode(string postalcode);

        geolookup GetGeoLookUpByPostalCodeByCountry(string postalcode, string countrycode);

        void AddGeoLookUp(geolookup lookupToAdd);

        void Update();
    }
}
