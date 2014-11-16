using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Models
{
    public class LinqDealerRepository : ILinqDealerRepository
    {
        MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();

        public dealer GetDealer(int DealerId)
        {
            return _datacontext.dealers.Where(m => m.dealerid == DealerId).FirstOrDefault();
        }
        
        public IList<dealer> GetDealers()
        {
            return _datacontext.dealers.ToList();
        }

        public dealer GetDealer(string name)
        {
            return _datacontext.dealers.Where(m => m.name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public bool DealerExists(string name)
        {
            return _datacontext.dealers.Where(m => m.name.ToLower() == name.ToLower()).Any();
        }

        public IList<dealer> GetDealers(int DealerId)
        {
            return _datacontext.dealers.Where(m => m.dealerid == DealerId).ToList();
        }

        public void AddDealer(dealer DealerToAdd)
        {
            _datacontext.dealers.InsertOnSubmit(DealerToAdd);
            _datacontext.SubmitChanges();
        }

        public void DeleteDealer(dealer Dealer)
        {
            _datacontext.dealers.DeleteOnSubmit(Dealer);
            _datacontext.SubmitChanges();
        }
        
        public dealer GetDealerBelow(int DealerId)
        {
            dealer relativeDealer = this.GetDealer(DealerId);
            return _datacontext.dealers.Where(v => v.sortorder > relativeDealer.sortorder).OrderBy(p => p.sortorder).First();
        }

        public dealer GetDealerAbove(int DealerId)
        {
            dealer relativeDealer = this.GetDealer(DealerId);
            return _datacontext.dealers.Where(v => v.sortorder < relativeDealer.sortorder).OrderByDescending(p => p.sortorder).First();
        }

        public IList<country> GetCountriesList()
        {
            return _datacontext.countries.ToList();
        }

        public country GetCountryById(int countryId)
        {
            return _datacontext.countries.Where(c => c.countryid == countryId).FirstOrDefault();
        }

        public geolookup GetGeoLookUpByPostalCode(string postalcode)
        {
            return _datacontext.geolookups.Where(l => l.postalcode.ToLower() == postalcode.ToLower()).FirstOrDefault();
        }

        public geolookup GetGeoLookUpByPostalCodeByCountry(string postalcode, string countrycode)
        {
            return _datacontext.geolookups.Where(l => l.postalcode.ToLower() == postalcode.ToLower() && l.countrycode == countrycode).FirstOrDefault();
        }

        public void AddGeoLookUp(geolookup lookupToAdd)
        {
            _datacontext.geolookups.InsertOnSubmit(lookupToAdd);
            _datacontext.SubmitChanges();
        }

        public void Update()
        {
            _datacontext.SubmitChanges();
        }
    }
}