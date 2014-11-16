using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using MotorMart.Core.Common;
using MotorMart.Core.Models;
using MotorMart.Core.Models.Validation;
using MotorMart.Cms.Models;
using Elmah;
using MotorMart.Core.Common.FileIO;
using System.IO;
using MotorMart.Core.HtmlHelpers;
using MotorMart.Cms.Areas.Vehicle.Models;
using AutoMapper;


namespace MotorMart.Cms.Areas.Vehicle.Services
{
    public class VehicleService : IVehicleService
    {
        private IValidationDictionary _validationDictionary;
        private ILinqMasterRepository _repository;
        private ILinqUserRepository _userRepository;
        private ILinqVehicleRepository _vehicleRepository;

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

        public bool GetVehicle(VehicleGetModel get)
        {
            bool success = false;
            if (_vehicleRepository.GetVehicle(get.vehicleid) != null)
            {
                success = true;
            }
            return success;
        }

        private bool GetVehicle(VehicleGetModel model, out vehicle Vehicle)
        {
            bool success = false;
            Vehicle = _vehicleRepository.GetVehicle(model.vehicleid);
            if (Vehicle != null)
            {
                success = true;
            }
            else
            {
                Vehicle = new vehicle();
            }
            return success;
        }

        private int GetVehilceAge(vehicle Vehicle)
        {
            return DateTime.Now.Year - Vehicle.yearofregistration;
        }
        
        #endregion

        #region IVehicle Admin service members

        public void PopulateAdminVehicleSearchViewModel(AdminVehicleSearchViewModel model)
        {
            try
            {
                if (model.vehiclesearch == null) model.vehiclesearch = new AdminVehicleSearchModel();

                // Set up search filters

                #region Select lists
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
                #endregion
                
                DateTime DateFrom;
                if (DateTime.TryParse(model.vehiclesearch.datefrom, out DateFrom)) model.vehiclesearch.DateFrom = DateFrom;

                DateTime DateTo;
                if (DateTime.TryParse(model.vehiclesearch.dateto, out DateTo)) model.vehiclesearch.DateTo = DateTo;

                IQueryable<vehicle> VehicleResults = _vehicleRepository.AdminSearchVehicles(model.vehiclesearch);
                List<AdminVehicleSearchResult> Results = new List<AdminVehicleSearchResult>();

                foreach (var item in VehicleResults)
                {
                    Results.Add(new AdminVehicleSearchResult
                    {
                        VehicleId = item.vehicleid,
                        Name = item.name,
                        DealerName = item.dealer.name,
                        VehicleReg = item.yearofregistration.ToString(),
                        Transmission = item.transmission.name,
                        Mileage = item.mileage.ToString(),
                        Age = GetVehilceAge(item).ToString(),
                        FuelType = item.fueltype.type,
                        Price = item.sellingprice.ToString(),
                        Enabled = item.enabled ? "Enabled" : "Disabled",
                        SortOrder = item.sortorder
                    });
                }

                model.SearchResults = new PagedList<AdminVehicleSearchResult>(Results, model.vehiclesearch.page <= 1 ? 0 : model.vehiclesearch.page - 1, 20);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        public void PopulateAdminVehicleViewModel(AdminVehicleViewModel model)
        {
            try
            {
                if(model == null) model = new AdminVehicleViewModel();               

                if (model.get != null)
                {
                    #region Images
                    model.vehicleimageadd = new VehicleImageAddModel { vehicleid = model.get.vehicleid };
                    model.vehicleimageedit = new VehicleImageEditModel { vehicleid = model.get.vehicleid, items = new List<VehicleImageItem>() };
                    
                    #endregion

                    vehicle Vehicle;
                    if (GetVehicle(model.get, out Vehicle))
                    {
                        model.CurrentVehicle = Vehicle;
                        model.ImageDimensions = VehicleHelper.ImageDimensions();

                        if (model.adddetails == null) model.adddetails = PopulateVehicleDetailsAddModel(Vehicle);
                        if (model.editdetails == null) model.editdetails = PopulateVehicleDetailsEditModel(Vehicle);
                        if (model.editsummarydetails == null) model.editsummarydetails = PopulateVehicleSummaryDetailsEditModel(Vehicle);
                        if (model.editdimensions == null) model.editdimensions = PopulateVehicleDimensionsEditModel(Vehicle);
                        if (model.editperformance == null) model.editperformance = PopulateVehiclePerformanceEditModel(Vehicle);
                        if (model.editsafetydetails == null) model.editsafetydetails = PopulateVehicleSafetyDetailsEditModel(Vehicle);
                        if (model.editfeatures == null) model.editfeatures = PopulateVehicleFeaturesEditModel(Vehicle);

                        foreach (var item in Vehicle.vehicleimages)
                        {
                            model.vehicleimageedit.items.Add(new VehicleImageItem { vehicleimageid = item.vehicleimageid, caption = item.caption, filename = item.filename });
                        }

                    }
                    else
                    {
                        if (model.adddetails == null) model.adddetails = new VehicleDetailsAddModel();                        
                    }

                    #region select lists
                    model.MakeSelect = SelectListHelper.GetAdminVehicleMakeSelect(_vehicleRepository.GetVehicleMakeList(), Vehicle.makeid);

                    model.ModelSelect = SelectListHelper.GetVehicleModelSelect(_vehicleRepository.GetVehicleModelList(), Vehicle.modelid);

                    model.FuelTypeSelect = SelectListHelper.GetFuelTypeSelect(_vehicleRepository.GetFuelTypeList(), Vehicle.fueltypeid);

                    model.BodyTypeSelect = SelectListHelper.GetBodyTypeSelect(_vehicleRepository.GetBodyTypeList(), Vehicle.bodytypeid);

                    model.TransmissionSelect = SelectListHelper.GetTransmissionSelect(_vehicleRepository.GetTransmissionList(), Vehicle.transmissionid);

                    model.ColorSelect = SelectListHelper.GetVehicleColorSelect(_vehicleRepository.GetColorList(), Vehicle.colorid);

                    model.DoorsSelect = SelectListHelper.GetNumberOfDoorsSelect(Vehicle.numberofdoors);

                    model.DealerSelect = SelectListHelper.GetDealerSelect(_vehicleRepository.GetDealerList(), Vehicle.dealerid ?? 0);

                    #endregion
                }

                model.VehicleList = _vehicleRepository.ListVehicles();
                
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("Vehicle", ex.Message);
            }
        }

        #region Models

        private VehicleDetailsAddModel PopulateVehicleDetailsAddModel(vehicle Vehicle)
        {
            VehicleDetailsAddModel model = new VehicleDetailsAddModel
            {
                
            };

            return model;
        }

        private VehicleDetailsEditModel PopulateVehicleDetailsEditModel(vehicle Vehicle)
        {
            VehicleDetailsEditModel model = new VehicleDetailsEditModel
            {
                vehicleid = Vehicle.vehicleid,
                reference = Vehicle.reference,
                name = Vehicle.name,
                makeid = Vehicle.makeid,
                modelid = Vehicle.modelid,
                transmisionid = Vehicle.transmissionid,
                fueltypeid = Vehicle.fueltypeid,
                bodytypeid = Vehicle.bodytypeid,
                colorid = Vehicle.colorid,
                dealerid = Vehicle.dealerid ?? 0,
                shortdescription = Vehicle.shortdescription,
                fulldescription = Vehicle.fulldescription,
                enabled = Vehicle.enabled
            };

            return model;
        }

        private VehicleSummaryDetailsEditModel PopulateVehicleSummaryDetailsEditModel(vehicle Vehicle)
        {
            VehicleSummaryDetailsEditModel model = new VehicleSummaryDetailsEditModel
            {
                vehicleid = Vehicle.vehicleid,
                numberofdoors = Vehicle.numberofdoors,
                numberofseats = Vehicle.numberofseats,
                mileage = Vehicle.mileage,
                dateofmanufacture = Vehicle.dateofmanufacture,
                enginesize = decimal.Round(Vehicle.enginesize ?? 0, 2),
                co2emissions = Vehicle.co2emissions,
                manufacturerwarrantyyears = Vehicle.manufacturerwarrantyyears,
                manufacturerwarrantymiles = Vehicle.manufacturerwarrantymiles,
                paintworkguaranteeyears = Vehicle.paintworkguaranteeyears,
                corrosionguaranteeyears = Vehicle.corrosionguaranteeyears,
                taxband = Vehicle.taxband,
                yearofregistration = Vehicle.yearofregistration,
                sellingprice = decimal.Round(Vehicle.sellingprice ?? decimal.Zero, 2),
                dateofsale = Vehicle.datesold
            };

            return model;
        }

        private VehicleDimensionsEditModel PopulateVehicleDimensionsEditModel(vehicle Vehicle)
        {
            VehicleDimensionsEditModel model = new VehicleDimensionsEditModel
            {
                vehicleid = Vehicle.vehicleid,
                dimensionsid = Vehicle.dimensionsid,
                height = Vehicle.dimension.height,
                wheelbase = Vehicle.dimension.wheelbase,
                width = Vehicle.dimension.width,
                widthincludingmirrors = Vehicle.dimension.widthincludingmirrors,
                fueltankcapacity = Vehicle.dimension.fueltankcapacity,
                grossvehicleweight = Vehicle.dimension.grossvehicleweight,
                luggagecapacitywithseatsdown = Vehicle.dimension.luggagecapacitywithseatsdown,
                luggagecapacitywithseatsup = Vehicle.dimension.luggagecapacitywithseatsup,
                maxloadingweight = Vehicle.dimension.maxloadingweight,
                maxroofload = Vehicle.dimension.maxroofload,
                maxtowingweightbraked = Vehicle.dimension.maxtowingweightbraked,
                maxtowingweightunbraked = Vehicle.dimension.maxtowingweightunbraked,
                minkerbweight = Vehicle.dimension.minkerbweight,
                kerbtokerbturningcircle = Vehicle.dimension.kerbtokerbturningcircle
            };

            return model;
        }

        private VehicleFeaturesEditModel PopulateVehicleFeaturesEditModel(vehicle Vehicle)
        {
            VehicleFeaturesEditModel model = new VehicleFeaturesEditModel
            {
                vehicleid = Vehicle.vehicleid,
                featuresid = Vehicle.featuresid,
                interiordetails = Vehicle.feature.interiordetails,
                exteriordetails = Vehicle.feature.exteriordetails
            };

            return model;
        }

        private VehiclePerformanceEditModel PopulateVehiclePerformanceEditModel(vehicle Vehicle)
        {
            VehiclePerformanceEditModel model = new VehiclePerformanceEditModel
            {
                vehicleid = Vehicle.vehicleid,
                performanceid = Vehicle.performanceid,
                urbanfuelconsumption = Vehicle.performance.urbanfuelconsumption,
                extraurbanfuelconsumption = Vehicle.performance.extraurbanfuelconsumption,
                combinedfuelconsumption = Vehicle.performance.combinedfuelconsumption,
                acceleration = Vehicle.performance.acceleration,
                topspeed = Vehicle.performance.topspeed,
                cylinders = Vehicle.performance.cylinders,
                valves = Vehicle.performance.valves,
                enginepower = Vehicle.performance.enginepower,
                enginetorque = Vehicle.performance.enginetorque
            };

            return model;
        }

        private VehicleSafetyDetailsEditModel PopulateVehicleSafetyDetailsEditModel(vehicle Vehicle)
        {
            VehicleSafetyDetailsEditModel model = new VehicleSafetyDetailsEditModel
            {
                vehicleid = Vehicle.vehicleid,
                safetydetailsid = Vehicle.safetydetailsid,
                details = Vehicle.safetydetail.details
            };

            return model;
        }

        #endregion

        public AdminVehicleDialogViewModel PopulateAdminVehicleDialogViewModel(VehicleGetModel getModel)
        {
            AdminVehicleDialogViewModel model = new AdminVehicleDialogViewModel();
            try
            {
                vehicle Vehicle;
                if (GetVehicle(new VehicleGetModel { vehicleid = getModel.vehicleid }, out Vehicle))
                {
                    model.delete = new VehicleDeleteModel { CurrentVehicle = Vehicle, vehicleid = Vehicle.vehicleid };
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("Dialog", ex.Message);
            }

            return model;
        }
        
        public bool AddVehicle(VehicleDetailsAddModel add)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            try
            {
                var AvailableVehicles = _vehicleRepository.ListVehicles().ToList();
                int SortOrder = AvailableVehicles.Count > 0 ? AvailableVehicles.OrderByDescending(v => v.sortorder).FirstOrDefault().sortorder + 1 : 0;

                //Automapper test
                //Mapper.CreateMap<VehicleDetailsAddModel, vehicle>();

                add.NewVehicle = new vehicle
                {
                    reference = StringHelpers.GenerateUrlReference(add.reference),
                    name = add.name,
                    makeid = add.makeid,
                    modelid = add.modelid,
                    transmissionid = add.transmisionid,
                    fueltypeid = add.fueltypeid,
                    bodytypeid = add.bodytypeid,
                    colorid = add.colorid,
                    dealerid = add.dealerid,
                    dimension = new dimension(),
                    feature = new feature(),
                    performance = new performance(),
                    safetydetail = new safetydetail(),
                    shortdescription = add.shortdescription,
                    fulldescription = add.fulldescription,
                    isnew = add.isnew,
                    enabled = add.enabled,
                    sortorder = SortOrder,
                    dateadded = DateTime.Now
                };

                _vehicleRepository.AddVehicle(add.NewVehicle);
                success = true;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("Error", ex.Message);
            }
            return success;
        }

        public bool EditVehicleDetails(VehicleDetailsEditModel edit)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            try
            {
                vehicle Vehicle;
                if(GetVehicle(new VehicleGetModel { vehicleid = edit.vehicleid }, out Vehicle))
                {
                    Vehicle.reference = StringHelpers.GenerateUrlReference(edit.reference);
                    Vehicle.name = edit.name;
                    Vehicle.makeid = edit.makeid;
                    Vehicle.modelid = edit.modelid;
                    Vehicle.transmissionid = edit.transmisionid;
                    Vehicle.fueltypeid = edit.fueltypeid;
                    Vehicle.bodytypeid = edit.bodytypeid;
                    Vehicle.colorid = edit.colorid;
                    Vehicle.dealerid = edit.dealerid;
                    Vehicle.shortdescription = edit.shortdescription;
                    Vehicle.fulldescription = edit.fulldescription;
                    Vehicle.isnew = edit.isnew;
                    Vehicle.enabled = edit.enabled;

                    _vehicleRepository.Update();
                    success = true;
                }
                else
                {
                    _validationDictionary.AddError("Error", "Unable to retrieve for editing");
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("Error", ex.Message);
            }
            return success;
        }

        public bool EditVehicleSummaryDetails(VehicleSummaryDetailsEditModel editsummary)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            try
            {
                vehicle Vehicle;
                if (GetVehicle(new VehicleGetModel { vehicleid = editsummary.vehicleid }, out Vehicle))
                {
                    Vehicle.numberofdoors = editsummary.numberofdoors;
                    Vehicle.numberofseats = editsummary.numberofseats;
                    Vehicle.mileage = editsummary.mileage;
                    Vehicle.dateofmanufacture = editsummary.dateofmanufacture;
                    Vehicle.enginesize = decimal.Round(editsummary.enginesize, 2);
                    Vehicle.co2emissions = editsummary.co2emissions;
                    Vehicle.manufacturerwarrantyyears = editsummary.manufacturerwarrantyyears;
                    Vehicle.manufacturerwarrantymiles = editsummary.manufacturerwarrantymiles;
                    Vehicle.paintworkguaranteeyears = editsummary.paintworkguaranteeyears;
                    Vehicle.corrosionguaranteeyears = editsummary.corrosionguaranteeyears;
                    Vehicle.taxband = editsummary.taxband;
                    Vehicle.yearofregistration = editsummary.yearofregistration;
                    Vehicle.sellingprice = decimal.Round(editsummary.sellingprice ?? decimal.Zero, 2);
                    Vehicle.datesold = editsummary.dateofsale;

                    _vehicleRepository.Update();
                    success = true;
                }
                else
                {
                    _validationDictionary.AddError("Error", "Unable to retrieve for editing");
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("Error", ex.Message);
            }
            return success;
        }

        public bool EditDimensions(VehicleDimensionsEditModel editdimensions)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            try
            {
                vehicle VehicleToEdit;
                if (GetVehicle(new VehicleGetModel { vehicleid = editdimensions.vehicleid }, out VehicleToEdit))
                {
                    VehicleToEdit.dimension.height = editdimensions.height;
                    VehicleToEdit.dimension.wheelbase = editdimensions.wheelbase;
                    VehicleToEdit.dimension.width = editdimensions.width;
                    VehicleToEdit.dimension.widthincludingmirrors = editdimensions.widthincludingmirrors;
                    VehicleToEdit.dimension.fueltankcapacity = editdimensions.fueltankcapacity;
                    VehicleToEdit.dimension.grossvehicleweight = editdimensions.grossvehicleweight;
                    VehicleToEdit.dimension.luggagecapacitywithseatsdown = editdimensions.luggagecapacitywithseatsdown;
                    VehicleToEdit.dimension.luggagecapacitywithseatsup = editdimensions.luggagecapacitywithseatsup;
                    VehicleToEdit.dimension.maxloadingweight = editdimensions.maxloadingweight;
                    VehicleToEdit.dimension.maxroofload = editdimensions.maxroofload;
                    VehicleToEdit.dimension.maxtowingweightbraked = editdimensions.maxtowingweightbraked;
                    VehicleToEdit.dimension.maxtowingweightunbraked = editdimensions.maxtowingweightunbraked;
                    VehicleToEdit.dimension.minkerbweight = editdimensions.minkerbweight;
                    VehicleToEdit.dimension.kerbtokerbturningcircle = editdimensions.kerbtokerbturningcircle;

                    _vehicleRepository.Update();
                    success = true;
                }
                else
                {
                    _validationDictionary.AddError("Error", "Unable to retrieve for editing");
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("Error", ex.Message);
            }
            return success;
        }

        public bool EditFeatures(VehicleFeaturesEditModel editfeatures)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            try
            {
                vehicle VehicleToEdit;
                if (GetVehicle(new VehicleGetModel { vehicleid = editfeatures.vehicleid }, out VehicleToEdit))
                {
                    VehicleToEdit.feature.interiordetails = editfeatures.interiordetails;
                    VehicleToEdit.feature.exteriordetails = editfeatures.exteriordetails;

                    _vehicleRepository.Update();
                    success = true;
                }
                else
                {
                    _validationDictionary.AddError("Error", "Unable to retrieve for editing");
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("Error", ex.Message);
            }
            return success;
        } 

        public bool EditPerformanceDetails(VehiclePerformanceEditModel editperformance)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            try
            {
                vehicle VehicleToEdit;
                if(GetVehicle(new VehicleGetModel { vehicleid = editperformance.vehicleid }, out VehicleToEdit))
                {
                    VehicleToEdit.performance.urbanfuelconsumption = editperformance.urbanfuelconsumption;
                    VehicleToEdit.performance.extraurbanfuelconsumption = editperformance.extraurbanfuelconsumption;
                    VehicleToEdit.performance.combinedfuelconsumption = editperformance.urbanfuelconsumption;
                    VehicleToEdit.performance.acceleration = editperformance.urbanfuelconsumption;
                    VehicleToEdit.performance.topspeed = editperformance.topspeed;
                    VehicleToEdit.performance.cylinders = editperformance.cylinders;
                    VehicleToEdit.performance.valves = editperformance.valves;
                    VehicleToEdit.performance.enginepower = editperformance.enginepower;
                    VehicleToEdit.performance.enginetorque = editperformance.enginetorque;

                    _vehicleRepository.Update();
                    success = true;
                }
                else
                {
                    _validationDictionary.AddError("Error", "Unable to retrieve for editing");
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("Error", ex.Message);
            }
            return success;
        }

        public bool EditSafetyDetails(VehicleSafetyDetailsEditModel editsafetydetails)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            try
            {
                vehicle VehicleToEdit;
                if (GetVehicle(new VehicleGetModel { vehicleid = editsafetydetails.vehicleid }, out VehicleToEdit))
                {
                    VehicleToEdit.safetydetail.details = editsafetydetails.details;

                    _vehicleRepository.Update();
                    success = true;
                }
                else
                {
                    _validationDictionary.AddError("Error", "Unable to retrieve for editing");
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("Error", ex.Message);
            }
            return success;
        } 

        public bool DeleteVehicle(VehicleDeleteModel model)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            try
            {
                vehicle Vehicle;
                if(GetVehicle(new VehicleGetModel { vehicleid = model.vehicleid}, out Vehicle))
                {
                    //Delete vehicle images if any exist
                    if (Vehicle.vehicleimages.Any())
                    {
                        foreach (var image in Vehicle.vehicleimages)
                        {
                            DeleteImage(new VehicleImageDeleteModel { vehicleid = Vehicle.vehicleid, vehicleimageid = image.vehicleimageid });
                        }
                    }
                    _vehicleRepository.DeleteVehicle(Vehicle);
                }
                success = _validationDictionary.IsValid;
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleError(ex, _validationDictionary);
            }
            return success;
        }

        public bool VehicleUp(int? Id)
        {
            bool success = true;
            try
            {
                vehicle vehicleSwap = _vehicleRepository.GetVehicle(Id.Value);
                vehicle vehicleSwapWith = _vehicleRepository.GetVehicleAbove(Id.Value);
                int tempOrder = vehicleSwap.sortorder;
                vehicleSwap.sortorder = vehicleSwapWith.sortorder;
                vehicleSwapWith.sortorder = tempOrder;
                _vehicleRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleError(ex, _validationDictionary);
            }
            return success;
        }

        public bool VehicleDown(int? Id)
        {
            bool success = true;
            try
            {
                vehicle vehicleSwap = _vehicleRepository.GetVehicle(Id.Value);
                vehicle vehicleSwapWith = _vehicleRepository.GetVehicleBelow(Id.Value);
                int tempOrder = vehicleSwap.sortorder;
                vehicleSwap.sortorder = vehicleSwapWith.sortorder;
                vehicleSwapWith.sortorder = tempOrder;
                _vehicleRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleError(ex, _validationDictionary);
            }
            return success;
        }

        #region Vehicle image(s)

        public bool AddImage(VehicleImageAddModel model)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            try
            {
                vehicle Vehicle;
                if (GetVehicle(new VehicleGetModel { vehicleid = model.vehicleid }, out Vehicle))
                {
                    FileUploadHelper FileUpload = new FileUploadHelper();
                    string TargetDirectory = VehicleHelper.ImageFileTargetDirectory();
                    FileUpload.FileName = String.Format("{0}_{1}", model.vehicleid, DateTime.Now.Ticks);
                    FileUpload.uploadFile(TargetDirectory, model.fileinput);
                    if (FileUpload.UploadComplete && FileUpload.ContainsFileToUpload)
                    {
                        string[] Dimensions = VehicleHelper.ImageDimensions();

                        for (int i = 0; i < Dimensions.Length; i++)
                        {
                            string TargetThumbDirectory = VehicleHelper.ImageFileThumbDirectory(FileHelper.ImageDimension(Dimensions[i]));
                            if (!Directory.Exists(TargetThumbDirectory)) Directory.CreateDirectory(TargetThumbDirectory);

                            ThumbnailGenerator thumb = new ThumbnailGenerator();
                            int Width = FileHelper.ImageDimensionWidth(FileHelper.ImageDimension(Dimensions[i]));
                            int Height = FileHelper.ImageDimensionHeight(FileHelper.ImageDimension(Dimensions[i]));

                            if (!thumb.GenerateThumbnail(String.Format("{0}{1}", TargetDirectory, FileUpload.FileName), String.Format("{0}{1}", TargetThumbDirectory, FileUpload.FileName), Width, Height))
                            {
                                _validationDictionary.AddArrayListErrors(thumb.Messages);
                            }
                        }

                        if (_validationDictionary.IsValid)
                        {
                            Vehicle.vehicleimages.Add(new vehicleimage
                            {
                                filename = FileUpload.FileName,
                                caption = model.caption
                            });
                            _vehicleRepository.Update();
                        }
                    }
                    else
                    {
                        _validationDictionary.AddArrayListErrors(FileUpload.Messages);
                    }
                }

                success = _validationDictionary.IsValid;
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleError(ex);
                _validationDictionary.AddError("Images", ex.Message);
            }
            return success;
        }

        public bool EditImages(VehicleImageEditModel model)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            try
            {
                vehicle Vehicle;
                if (GetVehicle(new VehicleGetModel { vehicleid = model.vehicleid }, out Vehicle))
                {
                    foreach (var item in model.items)
                    {
                        vehicleimage Image = _vehicleRepository.GetVehicleImage(item.vehicleimageid);
                        if (Image != null)
                        {
                            Image.caption = item.caption;
                            Image.sortorder = item.sortorder;
                        }
                        else
                        {
                            _validationDictionary.AddError("Image", "Unable to edit");
                        }
                    }
                    _vehicleRepository.Update();
                }

                success = _validationDictionary.IsValid;
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleError(ex);
                _validationDictionary.AddError("Images", ex.Message);
            }
            return success;
        }

        public bool DeleteImage(VehicleImageDeleteModel model)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            try
            {
                vehicle Vehicle;
                if (GetVehicle(new VehicleGetModel { vehicleid = model.vehicleid }, out Vehicle))
                {
                    vehicleimage Image = Vehicle.vehicleimages.Where(v => v.vehicleimageid == model.vehicleimageid).FirstOrDefault();
                    if (Image != null)
                    {
                        string TargetDirectory = VehicleHelper.ImageFileTargetDirectory();
                        if (File.Exists(TargetDirectory + Image.filename))
                        {
                            File.Delete(TargetDirectory + Image.filename);
                        }

                        string[] Dimensions = VehicleHelper.ImageDimensions();

                        for (int i = 0; i < Dimensions.Length; i++)
                        {
                            int Width = FileHelper.ImageDimensionWidth(FileHelper.ImageDimension(Dimensions[i]));
                            int Height = FileHelper.ImageDimensionHeight(FileHelper.ImageDimension(Dimensions[i]));
                            string TargetThumbDirectory = VehicleHelper.ImageFileThumbDirectory(FileHelper.ImageDimension(Dimensions[i]));

                            if (File.Exists(TargetThumbDirectory + Image.filename))
                            {
                                File.Delete(TargetThumbDirectory + Image.filename);
                            }
                        }
                        _vehicleRepository.DeleteVehicleImage(Image);
                    }
                }

                success = _validationDictionary.IsValid;
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleError(ex);
                _validationDictionary.AddError("Images", ex.Message);
            }
            return success;
        }
        
        #endregion

        #endregion
    }
}