using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqKit;

namespace MotorMart.Core.Models
{
    public class LinqVehicleRepository : ILinqVehicleRepository
    {
        MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();

        private IList<vehicle> _vehicles;

        public LinqVehicleRepository()
        {
            _vehicles = _datacontext.vehicles.ToList();
        }

        public vehicle GetVehicle(int vehicleId)
        {
            return _vehicles.Where(v => v.vehicleid == vehicleId).FirstOrDefault();
        }

        public IList<vehicle> ListVehicles()
        {
            return _vehicles.ToList();
        }

        public IList<make> GetVehicleMakeList()
        {
            return _datacontext.makes.ToList();
        }

        public IList<model> GetVehicleModelList()
        {
            return _datacontext.models.ToList();
        }

        public IList<fueltype> GetFuelTypeList()
        {
            return _datacontext.fueltypes.ToList();
        }

        public IList<bodytype> GetBodyTypeList()
        {
            return _datacontext.bodytypes.ToList();
        }

        public IList<transmission> GetTransmissionList()
        {
            return _datacontext.transmissions.ToList();
        }

        public IList<color> GetColorList()
        {
            return _datacontext.colors.ToList();
        }

        public IList<dealer> GetDealerList()
        {
            return _datacontext.dealers.ToList();
        }

        public void AddVehicle(vehicle vehicleToAdd)
        {
            _datacontext.vehicles.InsertOnSubmit(vehicleToAdd);
            _datacontext.SubmitChanges();
        }

        public void AddVehicleDealerDetails(dealer detailsToAdd)
        {
            _datacontext.dealers.InsertOnSubmit(detailsToAdd);
            _datacontext.SubmitChanges();
        }
        
        public dealer GetVehicleDealerDetails(int DealerId)
        {
            return _datacontext.dealers.Where(d => d.dealerid == DealerId).FirstOrDefault();
        }

        public void AddGeoLookUp(geolookup lookupToAdd)
        {
            _datacontext.geolookups.InsertOnSubmit(lookupToAdd);
            _datacontext.SubmitChanges();
        }

        public geolookup GetGeoLookUpByPostalCode(string postalcode)
        {
            return _datacontext.geolookups.Where(l => l.postalcode.ToLower() == postalcode.ToLower()).FirstOrDefault();
        }

        public geolookup GetGeoLookUpByPostalCodeByCountry(string postalcode, string countrycode)
        {
            return _datacontext.geolookups.Where(l => l.postalcode.ToLower() == postalcode.ToLower() && l.countrycode == countrycode).FirstOrDefault();
        }
        
        //Web search
        public IQueryable<vehicle> SearchVehicles(VehicleSearchModel model)
        {
            var predicate = PredicateBuilder.True<vehicle>();

            if (model.makeid > 0)
            {
                predicate = predicate.And(p => p.makeid == model.makeid);
            }
            if (model.modelid > 0)
            {
                predicate = predicate.And(p => p.modelid == model.modelid);
            }
            if (model.fueltypeid > 0)
            {
                predicate = predicate.And(p => p.fueltypeid == model.fueltypeid);
            }
            if (model.bodytypeid > 0)
            {
                predicate = predicate.And(p => p.bodytypeid == model.bodytypeid);
            }
            if (model.transmissionid > 0)
            {
                predicate = predicate.And(p => p.transmissionid == model.transmissionid);
            }
            if (model.colorid > 0)
            {
                predicate = predicate.And(p => p.colorid == model.colorid);
            }

            //Vehicle engine filters
            #region Engine size
            if (model.enginesize > 0)
            {
                if (model.enginesize == 1)
                {
                    predicate = predicate.And(p => p.enginesize <= 1);
                }
                if (model.enginesize == 2)
                {
                    predicate = predicate.And(p => p.enginesize >= 1 && p.enginesize <= 1.3M);
                }
                if (model.enginesize == 3)
                {
                    predicate = predicate.And(p => p.enginesize >= 1.4M && p.enginesize <= 1.6M);
                }
                if (model.enginesize == 4)
                {
                    predicate = predicate.And(p => p.enginesize >= 1.7M && p.enginesize <= 1.9M);
                }
                if (model.enginesize == 5)
                {
                    predicate = predicate.And(p => p.enginesize >= 2 && p.enginesize <= 2.5M);
                }
                if (model.enginesize == 6)
                {
                    predicate = predicate.And(p => p.enginesize >= 2.6M && p.enginesize <= 2.9M);
                }
                if (model.enginesize == 7)
                {
                    predicate = predicate.And(p => p.enginesize >= 3 && p.enginesize <= 3.9M);
                }
                if (model.enginesize == 8)
                {
                    predicate = predicate.And(p => p.enginesize >= 4 && p.enginesize <= 4.9M);
                }
                if (model.enginesize == 9)
                {
                    predicate = predicate.And(p => p.enginesize >= 5);
                }
                if (model.enginesize == 10)
                {
                    predicate = predicate.And(p => p.enginesize == null);
                }
            }
            #endregion

            //Vehicle Newness
            predicate = predicate.And(p => p.isnew == model.isnew);

            //Vehicle age filters
            #region Age filters
            if (model.vehicleage > 0)
            {                
                if (model.vehicleage == 9)
                {
                    //over 8 years old
                    predicate = predicate.And(p => p.dateofmanufacture <= DateTime.Now.AddYears(-9));
                }
                else if (model.vehicleage == 10)
                {
                    //unlisted
                    predicate = predicate.And(p => p.dateofmanufacture == null);
                }
                else
                {
                    predicate = predicate.And(p => p.dateofmanufacture >= DateTime.Now.AddYears(-(model.vehicleage ?? 0)) && p.dateofmanufacture <= DateTime.Now);
                }                
            }
            #endregion

            if (model.vehiclemileage > 0)
            {
                predicate = predicate.And(p => p.mileage <= model.vehiclemileage);
            }
            if (model.minprice > 0)
            {
                predicate = predicate.And(p => p.sellingprice >= model.minprice);
            }
            if (model.maxprice > 0)
            {
                predicate = predicate.And(p => p.sellingprice <= model.maxprice);
            }
            if (model.numberofdoors > 0)
            {
                predicate = predicate.And(p => p.numberofdoors == model.numberofdoors);
            }

            #region Sort By filters

            //if (model.sortby > 0)
            //{
            //    if (model.sortby == 1)
            //    {
            //        return _datacontext.vehicles.Where(predicate).OrderBy(v => v.sellingprice).AsQueryable();
            //    }
            //    if (model.sortby == 2)
            //    {
            //        return _datacontext.vehicles.Where(predicate).OrderByDescending(v => v.sellingprice).AsQueryable();
            //    }
            //    //distance furthest
            //    if (model.sortby == 3)
            //    {
            //    }
            //    //distance nearest
            //    if (model.sortby == 4)
            //    {
            //    }
            //}
            #endregion

            //return _datacontext.vehicles.Where(predicate).AsQueryable();
            var query = _datacontext.vehicles.Where(predicate).AsQueryable();
            string generatedSql = _datacontext.GetCommand(query).CommandText;
            int resultCount = query.Count();

            //Save the sql and count
            ProcessSearch(generatedSql, resultCount);

            return query;
        }

        public IList<vehicleimage> GetVehicleImages(int VehicleId)
        {
            return _datacontext.vehicleimages.Where(v => v.vehicleid == VehicleId).OrderBy(v=>v.sortorder).ToList();
        }

        public void ProcessSearch(string sqlString, int count)
        {
            searchtray trail = new searchtray
            {
                sqlstring = sqlString,
                resultcount = count,
                daterun = DateTime.Now
            };

            _datacontext.searchtrays.InsertOnSubmit(trail);
            Update();
        }

        //CMS search
        public IQueryable<vehicle> AdminSearchVehicles(AdminVehicleSearchModel model)
        {
            var predicate = PredicateBuilder.True<vehicle>();

            #region Dealer distance filters
            if (model.dealerdistance > 0)
            {
                if (model.dealerdistance == 1)
                {
                }
                if (model.dealerdistance == 2)
                {
                }
                if (model.dealerdistance == 3)
                {
                }
                if (model.dealerdistance == 4)
                {
                }
                if (model.dealerdistance == 5)
                {
                }
                if (model.dealerdistance == 6)
                {
                }
                if (model.dealerdistance == 7)
                {
                }
                if (model.dealerdistance == 8)
                {
                }
                if (model.dealerdistance == 9)
                {
                }
                if (model.dealerdistance == 10)
                {
                    
                }
            }
            #endregion

            if (model.makeid > 0)
            {
                predicate = predicate.And(p => p.makeid == model.makeid);
            }
            if (model.modelid > 0)
            {
                predicate = predicate.And(p => p.modelid == model.modelid);
            }
            if (model.fueltypeid > 0)
            {
                predicate = predicate.And(p => p.fueltypeid == model.fueltypeid);
            }
            if (model.bodytypeid > 0)
            {
                predicate = predicate.And(p => p.bodytypeid == model.bodytypeid);
            }
            if (model.transmissionid > 0)
            {
                predicate = predicate.And(p => p.transmissionid == model.transmissionid);
            }
            if (model.colorid > 0)
            {
                predicate = predicate.And(p => p.colorid == model.colorid);
            }

            //Vehicle engine filters
            #region Engine size
            if (model.enginesize > 0)
            {
                if (model.enginesize == 1)
                {
                    predicate = predicate.And(p => p.enginesize <= 1);
                }
                if (model.enginesize == 2)
                {
                    predicate = predicate.And(p => p.enginesize >= 1);
                    predicate = predicate.And(p => p.enginesize <= 1.3M);
                }
                if (model.enginesize == 3)
                {
                    predicate = predicate.And(p => p.enginesize >= 1.4M);
                    predicate = predicate.And(p => p.enginesize <= 1.6M);
                }
                if (model.enginesize == 4)
                {
                    predicate = predicate.And(p => p.enginesize >= 1.7M);
                    predicate = predicate.And(p => p.enginesize <= 1.9M);
                }
                if (model.enginesize == 5)
                {
                    predicate = predicate.And(p => p.enginesize >= 2);
                    predicate = predicate.And(p => p.enginesize <= 2.5M);
                }
                if (model.enginesize == 6)
                {
                    predicate = predicate.And(p => p.enginesize >= 2.6M);
                    predicate = predicate.And(p => p.enginesize <= 2.9M);
                }
                if (model.enginesize == 7)
                {
                    predicate = predicate.And(p => p.enginesize >= 3);
                    predicate = predicate.And(p => p.enginesize <= 3.9M);
                }
                if (model.enginesize == 8)
                {
                    predicate = predicate.And(p => p.enginesize >= 4);
                    predicate = predicate.And(p => p.enginesize <= 4.9M);
                }
                if (model.enginesize == 9)
                {
                    predicate = predicate.And(p => p.enginesize >= 5);
                }
                if (model.enginesize == 10)
                {
                    predicate = predicate.And(p => p.enginesize == null);
                }
            }
            #endregion

            #region Vehicle age filters
            if (model.vehicleage > 0)
            {
                if (model.vehicleage == 1)
                {
                    predicate = predicate.And(p => p.dateofmanufacture >= DateTime.Now.AddYears(-1));
                    predicate = predicate.And(p => p.dateofmanufacture <= DateTime.Now);
                }

                if (model.vehicleage == 2)
                {
                    predicate = predicate.And(p => p.dateofmanufacture >= DateTime.Now.AddYears(-2));
                    predicate = predicate.And(p => p.dateofmanufacture <= DateTime.Now);
                }

                if (model.vehicleage == 3)
                {
                    predicate = predicate.And(p => p.dateofmanufacture >= DateTime.Now.AddYears(-3));
                    predicate = predicate.And(p => p.dateofmanufacture <= DateTime.Now);
                }

                if (model.vehicleage == 4)
                {
                    predicate = predicate.And(p => p.dateofmanufacture >= DateTime.Now.AddYears(-4));
                    predicate = predicate.And(p => p.dateofmanufacture <= DateTime.Now);
                }

                if (model.vehicleage == 5)
                {
                    predicate = predicate.And(p => p.dateofmanufacture >= DateTime.Now.AddYears(-5));
                    predicate = predicate.And(p => p.dateofmanufacture <= DateTime.Now);
                }

                if (model.vehicleage == 6)
                {
                    predicate = predicate.And(p => p.dateofmanufacture >= DateTime.Now.AddYears(-6));
                    predicate = predicate.And(p => p.dateofmanufacture <= DateTime.Now);
                }


                if (model.vehicleage == 7)
                {
                    predicate = predicate.And(p => p.dateofmanufacture >= DateTime.Now.AddYears(-7));
                    predicate = predicate.And(p => p.dateofmanufacture <= DateTime.Now);
                }


                if (model.vehicleage == 8)
                {
                    predicate = predicate.And(p => p.dateofmanufacture >= DateTime.Now.AddYears(-8));
                    predicate = predicate.And(p => p.dateofmanufacture <= DateTime.Now);
                }

                //over 8 years old
                if (model.vehicleage == 9)
                {
                    predicate = predicate.And(p => p.dateofmanufacture >= DateTime.Now.AddYears(-9));
                }

                //unlisted
                if (model.vehicleage == 10)
                {
                    predicate = predicate.And(p => p.dateofmanufacture == null);
                }
            }
            #endregion

            if (model.vehiclemileage > 0)
            {
                predicate = predicate.And(p => p.mileage == model.vehiclemileage);
            }
            if (model.minprice > 0)
            {
                predicate = predicate.And(p => p.sellingprice >= model.minprice);
            }
            if (model.maxprice > 0)
            {
                predicate = predicate.And(p => p.sellingprice <= model.maxprice);
            }
            if (model.numberofdoors > 0)
            {
                predicate = predicate.And(p => p.numberofdoors == model.numberofdoors);
            }            

            #region Date added filters
            if (model.DateFrom.HasValue)
            {
                predicate = predicate.And(d => d.dateadded >= model.DateFrom.Value);
            }
            if (model.DateTo.HasValue)
            {
                predicate = predicate.And(d => d.dateadded <= model.DateTo.Value);
            }
            #endregion

            if (!String.IsNullOrEmpty(model.keywords))
            {
                var inner = PredicateBuilder.False<vehicle>();
                inner = inner.Or(p => p.name.Contains(model.keywords));
                inner = inner.Or(p => p.fulldescription.Contains(model.keywords));
                inner = inner.Or(p => p.feature.interiordetails.Contains(model.keywords));
                inner = inner.Or(p => p.feature.exteriordetails.Contains(model.keywords));                
                predicate = predicate.And(inner);
            }

            #region Sort By filters
            if (model.sortby > 0)
            {
                if (model.sortby == 1)
                {
                    return _datacontext.vehicles.Where(predicate).OrderBy(v => v.sellingprice).AsQueryable();
                }
                if (model.sortby == 2)
                {
                    return _datacontext.vehicles.Where(predicate).OrderByDescending(v => v.sellingprice).AsQueryable();
                }
                //distance furthest
                if (model.sortby == 3)
                {
                }
                //distance nearest
                if (model.sortby == 4)
                {
                }
            }
            #endregion


            return _datacontext.vehicles.Where(predicate).AsQueryable();
        }

        public vehicleimage GetVehicleImage(int VehicleImageId)
        {
            return _datacontext.vehicleimages.Where(a => a.vehicleimageid == VehicleImageId).FirstOrDefault();
        }

        public void DeleteVehicleImage(vehicleimage Image)
        {
            _datacontext.vehicleimages.DeleteOnSubmit(Image);
            _datacontext.SubmitChanges();
        }

        public void DeleteVehicle(vehicle Vehicle)
        {
            _datacontext.vehicleimages.DeleteAllOnSubmit(Vehicle.vehicleimages);
            _datacontext.dimensions.DeleteOnSubmit(Vehicle.dimension);
            _datacontext.features.DeleteOnSubmit(Vehicle.feature);
            _datacontext.performances.DeleteOnSubmit(Vehicle.performance);
            _datacontext.safetydetails.DeleteOnSubmit(Vehicle.safetydetail);
            _datacontext.vehicles.DeleteOnSubmit(Vehicle);
            _datacontext.SubmitChanges();
        }

        public vehicle GetVehicleBelow(int vehicleId)
        {
            vehicle relativeVehicle = this.GetVehicle(vehicleId);
            return _datacontext.vehicles.Where(v => v.sortorder > relativeVehicle.sortorder).OrderBy(p => p.sortorder).First();
        }

        public vehicle GetVehicleAbove(int vehicleId)
        {
            vehicle relativeVehicle = this.GetVehicle(vehicleId);
            return _datacontext.vehicles.Where(v => v.sortorder < relativeVehicle.sortorder).OrderByDescending(p => p.sortorder).First();
        }

        public void Update()
        {
            _datacontext.SubmitChanges();
        }
    }
}
