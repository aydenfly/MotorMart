using System;
using System.Linq;
using MotorMart.Core.Models.Validation;
using MotorMart.Core.Models;
using MotorMart.Cms.Models;
using MotorMart.Cms.Areas.Misc.Models;
using Elmah;
using MotorMart.Core.HtmlHelpers;
using MotorMart.Core.Common;
using System.IO;
using MotorMart.Core.Common.FileIO;
using System.Web;
using System.Web.Mvc;


namespace MotorMart.Cms.Areas.Misc.Services
{
    public class DealerService : IDealerService
    {
        private IValidationDictionary _validationDictionary;
        private ILinqDealerRepository _dealerRepository;

        public DealerService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new LinqDealerRepository())
        {
        }

        public DealerService(IValidationDictionary validationDictionary, LinqDealerRepository dealerRepository)
        {
            _validationDictionary = validationDictionary;
            _dealerRepository = dealerRepository;
        }

        #region Helpers

        private string GetCoordinates(string coordinates, string postcode, int countryid)
        {
            if (String.IsNullOrEmpty(coordinates))
            {
                if (!String.IsNullOrEmpty(postcode) && countryid > 0)
                {
                    country Country = _dealerRepository.GetCountryById(countryid);
                    if (Country != null)
                    {
                        geolookup lookup = _dealerRepository.GetGeoLookUpByPostalCodeByCountry(postcode, Country.code);
                        if (lookup == null)
                        {
                            coordinates = DistanceHelper.LookUpCoordinates(postcode, Country.code);
                            geolookup NewlookUp = new geolookup
                            {
                                postalcode = postcode,
                                countrycode = Country.code,
                                coordinates = coordinates
                            };
                        }
                        else
                        {
                            coordinates = lookup.coordinates;
                        }
                    }
                }
            }
            return coordinates;
        }

        public bool GetDealer(DealerGetModel get, out dealer Dealer)
        {
            bool success = false;
            Dealer = _dealerRepository.GetDealer(get.dealerid);
            if (Dealer != null)
            {
                success = true;
            }
            return success;
        }

        private bool UploadVehicleDealerLogo(HttpPostedFileBase logo, bool success, out string fileName)
        {
            fileName = String.Empty;
            
            FileUploadHelper FileUpload = new FileUploadHelper();
            string TargetDirectory = VehicleDealerHelper.ImageFileTargetDirectory();
            FileUpload.FileName = Guid.NewGuid().ToString();
            FileUpload.uploadFile(TargetDirectory, logo);
            if (FileUpload.UploadComplete && FileUpload.ContainsFileToUpload)
            {
                string[] Dimensions = VehicleDealerHelper.ImageDimensions();

                for (int i = 0; i < Dimensions.Length; i++)
                {
                    string TargetThumbDirectory = VehicleDealerHelper.ImageFileThumbDirectory(FileHelper.ImageDimension(Dimensions[i]));
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
                    fileName = FileUpload.FileName;
                    success = true;
                }
            }
            else
            {
                _validationDictionary.AddArrayListErrors(FileUpload.Messages);
            }
            return success;
        }

        private bool DeleteDealerLogo(string filename)
        {
            bool success = false;

            string[] Dimensions = VehicleDealerHelper.ImageDimensions();

            for (int i = 0; i < Dimensions.Length; i++)
            {
                string TargetThumbDirectory = VehicleDealerHelper.ImageFileThumbDirectory(FileHelper.ImageDimension(Dimensions[i]));
                if (File.Exists(TargetThumbDirectory + "/" + filename))
                {
                    File.Delete(TargetThumbDirectory + "/" + filename);
                    success = true;
                }
            }
            return success;
        }

        private bool DealerAlreadyExists(string name)
        {
            return _dealerRepository.DealerExists(name.Trim());
        }

        private bool DealerAlreadyExists(int dealerId, string name)
        {
            bool exists = false;
            if (_dealerRepository.GetDealer(name.Trim()) != null)
            {
                if (dealerId != _dealerRepository.GetDealer(name.Trim()).dealerid)
                {
                    exists = true;
                }
            }
            return exists;
        }
        

        #endregion

        #region IVehicle Model service members

        public void PopulateDealerViewModel(DealerViewModel model)
        {
            try
            {
                if (model == null) model = new DealerViewModel();

                if (model.get != null)
                {
                    dealer Dealer;
                    if (GetDealer(model.get, out Dealer))
                    {
                        model.CurrentDealer = Dealer;

                        if (model.add == null) model.add = PopulateDealerAddModel(Dealer);
                        if (model.edit == null) model.edit = PopulateDealerEditModel(Dealer);
                        model.delete = new DealerDeleteModel { dealerid = Dealer.dealerid, CurrentDealer = Dealer };

                        model.CountrySelect = SelectListHelper.GetCountrySelect(_dealerRepository.GetCountriesList(), Dealer.countryid ?? 0);
                    }
                    else
                    {
                        if (model.add == null) model.add = PopulateDealerAddModel(new dealer());
                        if (model.edit == null) model.edit = PopulateDealerEditModel(new dealer());
                        model.CountrySelect = SelectListHelper.GetCountrySelect(_dealerRepository.GetCountriesList(), 0);
                    }
                }
                else
                {
                    model.CountrySelect = SelectListHelper.GetCountrySelect(_dealerRepository.GetCountriesList(), 0);
                }
                
                model.DealersList = _dealerRepository.GetDealers();
                model.DealerResults = new PagedList<dealer>(model.DealersList.OrderBy(a => a.sortorder).ToList(), model.page <= 1 ? 0 : model.page - 1, 20);
                
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        #endregion

        #region Models

        public DealerAddModel PopulateDealerAddModel(dealer Dealer)
        {
            DealerAddModel model = new DealerAddModel
            {
            };
            return model;
        }

        public DealerEditModel PopulateDealerEditModel(dealer Dealer)
        {
            DealerEditModel model = new DealerEditModel
            {
                dealerid = Dealer.dealerid,
                name = Dealer.name,
                owneraddress = Dealer.address,
                ownerpostcode = Dealer.postcode,
                coordinates = Dealer.coordinates,
                website = Dealer.website,
                filename = Dealer.logo
            };
            return model;
        }

        #endregion

        public bool AddDealer(DealerAddModel add)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            if (DealerAlreadyExists(add.name))
            {
                _validationDictionary.AddError("Error", "The dealer name supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {
                try
                {
                    var AvailableDealers = _dealerRepository.GetDealers().ToList();
                    int SortOrder = AvailableDealers.Count > 0 ? AvailableDealers.OrderByDescending(v => v.sortorder).FirstOrDefault().sortorder + 1 : 0;

                    //Get dealer coordinates
                    add.coordinates = GetCoordinates(add.coordinates, add.ownerpostcode, add.countryid ?? 0);

                    //Upload logo if supplied
                    if (add.logo != null && !String.IsNullOrEmpty(add.logo.FileName))
                    {
                        string filename;
                        success = UploadVehicleDealerLogo(add.logo, success, out filename);

                        if (success)
                        {
                            add.NewDealer = new dealer
                            {
                                name = add.name,
                                address = add.owneraddress,
                                postcode = add.ownerpostcode,
                                countryid = add.countryid,                                
                                coordinates = add.coordinates,
                                website = add.website,
                                logo = filename,
                                sortorder = SortOrder
                            };

                            _dealerRepository.AddDealer(add.NewDealer);
                            success = true;
                        }
                    }
                    else
                    {
                        add.NewDealer = new dealer
                        {
                            name = add.name,
                            address = add.owneraddress,
                            postcode = add.ownerpostcode,
                            countryid = add.countryid,
                            coordinates = add.coordinates,
                            website = add.website,
                            logo = String.Empty,
                            sortorder = SortOrder
                        };

                        _dealerRepository.AddDealer(add.NewDealer);
                        success = true;
                    }

                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            return success;
        }

        public bool EditDealer(DealerEditModel edit)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;


            if (DealerAlreadyExists(edit.dealerid, edit.name))
            {
                _validationDictionary.AddError("Error", "The dealer name supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {
                try
                {
                    dealer DealerToEdit;
                    if (GetDealer(new DealerGetModel { dealerid = edit.dealerid }, out DealerToEdit))
                    {
                        //Get dealer coordinates
                        edit.coordinates = GetCoordinates(edit.coordinates, edit.ownerpostcode, edit.countryid ?? 0);

                        //If dealer has logo, delete old and Upload new one
                        if (edit.logo != null && !String.IsNullOrEmpty(edit.logo.FileName))
                        {
                            bool deletesuccessful = true;

                            if (!String.IsNullOrEmpty(DealerToEdit.logo))
                            {                                
                                deletesuccessful = DeleteDealerLogo(DealerToEdit.logo);
                            }
                            if (deletesuccessful)
                            {
                                string fileName;
                                success = UploadVehicleDealerLogo(edit.logo, success, out fileName);
                                if (success)
                                {
                                    DealerToEdit.name = edit.name;
                                    DealerToEdit.address = edit.owneraddress;
                                    DealerToEdit.postcode = edit.ownerpostcode;
                                    DealerToEdit.countryid = edit.countryid;
                                    DealerToEdit.coordinates = edit.coordinates;
                                    DealerToEdit.website = edit.website;
                                    DealerToEdit.logo = fileName;
                                }
                            }
                            else
                            {
                                _validationDictionary.AddError("Error", "We failed to delete the current logo. Please try again later.");
                            }
                        }
                        else
                        {
                            DealerToEdit.name = edit.name;
                            DealerToEdit.address = edit.owneraddress;
                            DealerToEdit.postcode = edit.ownerpostcode;
                            DealerToEdit.countryid = edit.countryid;
                            DealerToEdit.coordinates = edit.coordinates;
                            DealerToEdit.website = edit.website;
                        }

                        _dealerRepository.Update();
                        success = true;
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                    _validationDictionary.AddError("Error", ex.Message);
                }
            }
            return success;
        }

        public bool DeleteDealer(DealerDeleteModel model)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            try
            {
                dealer DealerToDelete;
                if (GetDealer(new DealerGetModel { dealerid = model.dealerid }, out DealerToDelete))
                {
                    if (!String.IsNullOrEmpty(DealerToDelete.logo))
                    {
                        if (DeleteDealerLogo(DealerToDelete.logo))
                        {
                            _dealerRepository.DeleteDealer(DealerToDelete);
                        }
                    }
                    else
                    {
                        _dealerRepository.DeleteDealer(DealerToDelete);
                    }
                }
                success = _validationDictionary.IsValid;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;

        }

        public bool DealerUp(int? Id)
        {
            bool success = true;
            try
            {
                dealer dealerSwap = _dealerRepository.GetDealer(Id.Value);
                dealer dealerSwapWith = _dealerRepository.GetDealerAbove(Id.Value);
                int tempOrder = dealerSwap.sortorder;
                dealerSwap.sortorder = dealerSwapWith.sortorder;
                dealerSwapWith.sortorder = tempOrder;
                _dealerRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }

        public bool DealerDown(int? Id)
        {
            bool success = true;
            try
            {
                dealer dealerSwap = _dealerRepository.GetDealer(Id.Value);
                dealer dealerSwapWith = _dealerRepository.GetDealerBelow(Id.Value);
                int tempOrder = dealerSwap.sortorder;
                dealerSwap.sortorder = dealerSwapWith.sortorder;
                dealerSwapWith.sortorder = tempOrder;
                _dealerRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }
        
    }
}