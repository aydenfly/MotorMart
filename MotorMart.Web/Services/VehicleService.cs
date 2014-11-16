using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Core.Common;
using MotorMart.Core.Models;
using MotorMart.Core.Models.Validation;
using MotorMart.Core.HtmlHelpers;
using MotorMart.Core.Enums;
using Elmah;
using MotorMart.Web.Models;
using System.Web.UI;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;


namespace MotorMart.Web.Services
{
    public class VehicleService : IVehicleService
    {
        private IValidationDictionary _validationDictionary;
        private ILinqMasterRepository _repository;
        private ILinqUserRepository _userRepository;
        private ILinqVehicleRepository _vehicleRepository;

        private IList<vehicle> _entireVehicleList;

        public VehicleService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new LinqMasterRepository(), new LinqUserRepository(), new LinqVehicleRepository())
        { }

        public VehicleService(IValidationDictionary validationDictionary, ILinqMasterRepository repository, ILinqUserRepository userRepository, ILinqVehicleRepository vehicleRepository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
            _userRepository = userRepository;
            _vehicleRepository = vehicleRepository;
        }

        #region Helpers

        private bool GetVehicle(VehicleGetModel model, out vehicle Vehicle)
        {
            Vehicle = _vehicleRepository.GetVehicle(model.vehicleid);
            return Vehicle.vehicleid > 0;
        }

        private static void SortSearchResults(VehicleSearchViewModel model, List<VehicleSearchViewModel.VehicleSearchResult> Results)
        {
            if (model.vehiclesearch.sortby > 0)
            {
                if (model.vehiclesearch.sortby == 1)
                {
                    model.SearchResults = new PagedList<VehicleSearchViewModel.VehicleSearchResult>(Results.OrderBy(a => a.Price).ToList(), model.vehiclesearch.page <= 1 ? 0 : model.vehiclesearch.page - 1, 20);
                }
                else if (model.vehiclesearch.sortby == 2)
                {
                    model.SearchResults = new PagedList<VehicleSearchViewModel.VehicleSearchResult>(Results.OrderByDescending(a => a.Price).ToList(), model.vehiclesearch.page <= 1 ? 0 : model.vehiclesearch.page - 1, 20);
                }
                else if (model.vehiclesearch.sortby == 3)
                {
                    model.SearchResults = new PagedList<VehicleSearchViewModel.VehicleSearchResult>(Results.OrderByDescending(a => a.Distance).ToList(), model.vehiclesearch.page <= 1 ? 0 : model.vehiclesearch.page - 1, 20);
                }
                else if (model.vehiclesearch.sortby == 4)
                {
                    model.SearchResults = new PagedList<VehicleSearchViewModel.VehicleSearchResult>(Results.OrderBy(a => a.Distance).ToList(), model.vehiclesearch.page <= 1 ? 0 : model.vehiclesearch.page - 1, 20);
                }
            }
            else
            {

                model.SearchResults = new PagedList<VehicleSearchViewModel.VehicleSearchResult>(Results.OrderByDescending(a => a.Name).ToList(), model.vehiclesearch.page <= 1 ? 0 : model.vehiclesearch.page - 1, 20);
            }
        }        
               
        #endregion

        #region IVehicle service members

        public List<model> GetMakeModels(int makeid)
        {
            List<model> MakeModels = new List<model>();
            
            MakeModels = _vehicleRepository.GetVehicleModelList().Where(m => m.makeid == makeid).OrderBy(m=>m.name).ToList();

            return MakeModels;
        }

        public IList<vehicle> ListVehicles()
        {
            try
            {
                _entireVehicleList = _vehicleRepository.ListVehicles();
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleError(ex);
            }

            return _entireVehicleList ?? (_entireVehicleList = new List<vehicle>());
        }

        public void PopulateVehicleViewModel(MotorMart.Web.Models.VehicleViewModel model)
        {
            try
            {
                vehicle Vehicle;
                if (!GetVehicle(model.get, out Vehicle)) return;
                model.CurrentVehicle = Vehicle;
                model.VehicleList = _vehicleRepository.ListVehicles();
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleError(ex);
                _validationDictionary.AddError("Article", ex.Message);
            }
        }

        public void PopulateVehicleSearchViewModel(VehicleSearchViewModel model)
        {
            try
            {
                if (model == null) model = new VehicleSearchViewModel();

                //set up search filters
                model.vehiclesearch.MakeSelect = SelectListHelper.GetVehicleMakeSelect(_vehicleRepository.GetVehicleMakeList(), model.vehiclesearch.makeid ?? 0);
                model.vehiclesearch.ModelSelect = SelectListHelper.GetVehicleModelSelect(_vehicleRepository.GetVehicleModelList(), model.vehiclesearch.modelid ?? 0);
                model.vehiclesearch.FuelTypeSelect = SelectListHelper.GetFuelTypeSelect(_vehicleRepository.GetFuelTypeList(), model.vehiclesearch.fueltypeid ?? 0);
                model.vehiclesearch.AgeSelect = SelectListHelper.GetAgeSelect(model.vehiclesearch.vehicleage ?? 0);
                model.vehiclesearch.MileageSelect = SelectListHelper.GetMileageSelect(model.vehiclesearch.vehiclemileage ?? 0);
                model.vehiclesearch.DistanceSelect = SelectListHelper.GetDistanceSelect(model.vehiclesearch.dealerdistance ?? 0);
                model.vehiclesearch.EngineSizeSelect = SelectListHelper.GetEngineSizeSelect(model.vehiclesearch.enginesize ?? 0);
                model.vehiclesearch.BodyTypeSelect = SelectListHelper.GetBodyTypeSelect(_vehicleRepository.GetBodyTypeList(), model.vehiclesearch.bodytypeid ?? 0);
                model.vehiclesearch.TransmissionSelect = SelectListHelper.GetTransmissionSelect(_vehicleRepository.GetTransmissionList(), model.vehiclesearch.transmissionid ?? 0);
                model.vehiclesearch.ColorSelect = SelectListHelper.GetVehicleColorSelect(_vehicleRepository.GetColorList(), model.vehiclesearch.colorid ?? 0);
                model.vehiclesearch.DoorsSelect = SelectListHelper.GetNumberOfDoorsSelect(model.vehiclesearch.numberofdoors ?? 0);
                model.vehiclesearch.MinPriceSelect = SelectListHelper.GetMinPriceSelect(model.vehiclesearch.minprice ?? 0);
                model.vehiclesearch.MaxPriceSelect = SelectListHelper.GetMaxPriceSelect(model.vehiclesearch.maxprice ?? 0);
                model.vehiclesearch.SortBySelect = SelectListHelper.GetSortBySelect(model.vehiclesearch.sortby);
                model.SearchResults = new PagedList<VehicleSearchViewModel.VehicleSearchResult>(new List<VehicleSearchViewModel.VehicleSearchResult>(), model.vehiclesearch.page <= 1 ? 0 : model.vehiclesearch.page - 1, 20);

                if (_validationDictionary.IsValid)
                {
                    if (!String.IsNullOrEmpty(model.vehiclesearch.postcode))
                    {
                        //Visitors coordinates
                        string visitorCoords = String.Empty;
                        geolookup lookup = _vehicleRepository.GetGeoLookUpByPostalCode(model.vehiclesearch.postcode);
                        if (lookup != null)
                        {
                            visitorCoords = lookup.coordinates;
                        }
                        else
                        {
                            visitorCoords = DistanceHelper.LookUpCoordinates(model.vehiclesearch.postcode);
                                                        
                            //Add coordinates and code to lookup table
                            string [] coords = visitorCoords.Split(new char[] { ','});
                            geolookup NewLookUp = new geolookup
                            {
                                postalcode = model.vehiclesearch.postcode,
                                coordinates = String.Format("{0},{1}", coords[0], coords[1]),
                                countrycode = coords.Length == 3 ? coords[2] : String.Empty
                            };
                            
                            _vehicleRepository.AddGeoLookUp(NewLookUp);

                            visitorCoords = String.Format("{0},{1}", coords[0], coords[1]);
                        }
                        string[] visitorCoordsArray = visitorCoords.Split(new char[] { ',' });
                        double vlat = Convert.ToDouble(visitorCoordsArray[0]);
                        double vlng = Convert.ToDouble(visitorCoordsArray[1]);

                        if (visitorCoordsArray.Length == 2)
                        {
                            model.vehiclesearch.visitorlat = vlat;
                            model.vehiclesearch.visitorlng = vlng;
                        }

                        IQueryable<vehicle> VehicleResults = _vehicleRepository.SearchVehicles(model.vehiclesearch);
                        List<VehicleSearchViewModel.VehicleSearchResult> Results = new List<VehicleSearchViewModel.VehicleSearchResult>();

                        foreach (var item in VehicleResults)
                        {
                            string[] dealerCoords = item.dealer.coordinates.Split(new char[] { ',' });
                            double dlat = Convert.ToDouble(dealerCoords[0]);
                            double dlng = Convert.ToDouble(dealerCoords[1]);
                            
                            double distance = 0;
                            if(visitorCoordsArray.Length == 2 && dealerCoords.Length == 2)
                            {
                                distance = DistanceHelper.GetDistance(vlat, vlng, dlat, dlng, DistanceHelper.DistanceUnits.Miles.ToString());
                            }

                            //Narrow down results by selected range
                            if (model.vehiclesearch.dealerdistance == null || model.vehiclesearch.dealerdistance <= 0)
                            {
                                PopulateResultsList(Results, item, distance);
                            }
                            else if (distance <= model.vehiclesearch.dealerdistance)
                            {
                                PopulateResultsList(Results, item, distance);
                            }
                        }

                        SortSearchResults(model, Results);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        private static void PopulateResultsList(List<VehicleSearchViewModel.VehicleSearchResult> Results, vehicle item, double distance)
        {
            Results.Add(new VehicleSearchViewModel.VehicleSearchResult
            {
                VehicleId = item.vehicleid,
                YearReg = item.yearofregistration,
                MainPhoto = item.vehicleimages.Any() ? item.vehicleimages.FirstOrDefault().filename : String.Empty,
                Name = item.name,
                DealerName = item.dealer.name,
                DealerLogo = item.dealer.logo,
                ShortDescription = item.shortdescription,
                Price = decimal.Round(item.sellingprice ?? 0, 2),
                Transmission = item.transmission.name,
                EngineSize = item.enginesize.ToString(),
                Mileage = item.mileage,
                FuelType = item.fueltype.type,
                Distance = decimal.Round((decimal)distance, 2).ToString()
            });
        }

        public vehicle GetVehicle(VehicleGetModel get)
        {
            try
            {
                return _vehicleRepository.GetVehicle(get.vehicleid);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return new vehicle();
            }
            
        }

        #endregion
    }
}