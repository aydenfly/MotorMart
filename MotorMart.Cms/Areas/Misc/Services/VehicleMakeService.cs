using System;
using System.Linq;
using MotorMart.Core.Models.Validation;
using MotorMart.Core.Models;
using MotorMart.Cms.Models;
using MotorMart.Cms.Areas.Misc.Models;
using Elmah;
using MotorMart.Core.HtmlHelpers;
using MotorMart.Core.Common.FileIO;
using MotorMart.Core.Common;
using System.IO;
using System.Web;
using System.Web.Mvc;


namespace MotorMart.Cms.Areas.Misc.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private IValidationDictionary _validationDictionary;
        private ILinqVehicleRepository _vehicleRepository;
        private ILinqVehicleMakeRepository _vehicleMakeRepository;

        public VehicleMakeService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new LinqVehicleRepository(), new LinqVehicleMakeRepository())
        {
        }

        public VehicleMakeService(IValidationDictionary validationDictionary, LinqVehicleRepository vehicleRepository, LinqVehicleMakeRepository bodyTypeRepository)
        {
            _validationDictionary = validationDictionary;
            _vehicleMakeRepository = bodyTypeRepository;
            _vehicleRepository = vehicleRepository;
        }

        #region Helpers

        public bool GetVehicleMake(VehicleMakeGetModel get, out make VehicleMake)
        {
            bool success = false;
            VehicleMake = _vehicleMakeRepository.GetVehicleMake(get.makeid);
            if (VehicleMake != null)
            {
                success = true;
            }
            return success;
        }

        private bool UploadVehicleMakeLogo(HttpPostedFileBase logo, bool success, out string fileName)
        {
            fileName = String.Empty;

            FileUploadHelper FileUpload = new FileUploadHelper();
            string TargetDirectory = VehicleMakeHelper.ImageFileTargetDirectory();
            FileUpload.FileName = Guid.NewGuid().ToString();
            FileUpload.uploadFile(TargetDirectory, logo);
            if (FileUpload.UploadComplete && FileUpload.ContainsFileToUpload)
            {
                string[] Dimensions = VehicleMakeHelper.ImageDimensions();

                for (int i = 0; i < Dimensions.Length; i++)
                {
                    string TargetThumbDirectory = VehicleMakeHelper.ImageFileThumbDirectory(FileHelper.ImageDimension(Dimensions[i]));
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

        private bool DeleteMakeLogo(string filename)
        {
            bool success = false;

            string[] Dimensions = VehicleMakeHelper.ImageDimensions();

            for (int i = 0; i < Dimensions.Length; i++)
            {
                string TargetThumbDirectory = VehicleMakeHelper.ImageFileThumbDirectory(FileHelper.ImageDimension(Dimensions[i]));
                if (File.Exists(TargetThumbDirectory + "/" + filename))
                {
                    File.Delete(TargetThumbDirectory + "/" + filename);
                    success = true;
                }
            }
            return success;
        }

        private bool VehicleMakeAlreadyExists(string name)
        {
            return _vehicleMakeRepository.VehicleMakeExists(name.Trim());
        }

        private bool VehicleMakeAlreadyExists(int makeId, string name)
        {
            bool exists = false;
            if (_vehicleMakeRepository.GetVehicleMake(name.Trim()) != null)
            {
                if (makeId != _vehicleMakeRepository.GetVehicleMake(name.Trim()).makeid)
                {
                    exists = true;
                }
            }
            return exists;
        }
        
        #endregion

        #region IVehicle Model service members

        public void PopulateVehicleMakeViewModel(VehicleMakeViewModel model)
        {
            try
            {
                if (model == null) model = new VehicleMakeViewModel();

                if (model.get != null)
                {
                    make VehicleMake;
                    if (GetVehicleMake(model.get, out VehicleMake))
                    {
                        model.CurrentVehicleMake = VehicleMake;

                        if (model.add == null) model.add = PopulateVehicleMakeAddModel(VehicleMake);
                        if (model.edit == null) model.edit = PopulateVehicleMakeEditModel(VehicleMake);
                        model.delete = new VehicleMakeDeleteModel { makeid = VehicleMake.makeid, CurrentMake = VehicleMake };
                    }
                    else
                    {
                        if (model.add == null) model.add = PopulateVehicleMakeAddModel(new make());
                        if (model.edit == null) model.edit = PopulateVehicleMakeEditModel(new make());
                    }
                }
                
                model.VehicleMakesList = _vehicleMakeRepository.GetVehicleMakes();
                model.MakeResults = new PagedList<make>(model.VehicleMakesList.OrderBy(a => a.sortorder).ToList(), model.page <= 1 ? 0 : model.page - 1, 20);

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        #endregion

        #region Models

        public VehicleMakeAddModel PopulateVehicleMakeAddModel(make Model)
        {
            VehicleMakeAddModel make = new VehicleMakeAddModel
            {
            };
            return make;
        }

        public VehicleMakeEditModel PopulateVehicleMakeEditModel(make Model)
        {
            VehicleMakeEditModel make = new VehicleMakeEditModel
            {
                makeid = Model.makeid,
                name = Model.name,
                filename = Model.logo
            };
            return make;
        }

        #endregion

        public bool AddVehicleMake(VehicleMakeAddModel add)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            if (VehicleMakeAlreadyExists(add.name))
            {
                _validationDictionary.AddError("Error", "The vehicle make supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {
                try
                {
                    var AvailableVehicleMakes = _vehicleMakeRepository.GetVehicleMakes().ToList();
                    int SortOrder = AvailableVehicleMakes.Count > 0 ? AvailableVehicleMakes.OrderByDescending(v => v.sortorder).FirstOrDefault().sortorder + 1 : 0;

                    //Upload logo if supplied
                    if (add.logo != null && !String.IsNullOrEmpty(add.logo.FileName))
                    {
                        string filename;
                        success = UploadVehicleMakeLogo(add.logo, success, out filename);

                        if (success)
                        {
                            add.NewVehicleMake = new make
                            {
                                name = add.name,
                                sortorder = SortOrder,
                                logo = filename
                            };

                            _vehicleMakeRepository.AddVehicleMake(add.NewVehicleMake);
                            success = true;
                        }
                    }
                    else
                    {
                        add.NewVehicleMake = new make
                        {
                            name = add.name,
                            sortorder = SortOrder,
                            logo = String.Empty
                        };

                        _vehicleMakeRepository.AddVehicleMake(add.NewVehicleMake);
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
                
        public bool EditVehicleMake(VehicleMakeEditModel edit)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            if (VehicleMakeAlreadyExists(edit.makeid, edit.name))
            {
                _validationDictionary.AddError("Error", "The vehicle make supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {
                try
                {
                    make VehicleMake;
                    if (GetVehicleMake(new VehicleMakeGetModel { makeid = edit.makeid }, out VehicleMake))
                    {
                        //If make has logo, delete old and Upload new
                        if (edit.logo != null && !String.IsNullOrEmpty(edit.logo.FileName))
                        {
                            if (!String.IsNullOrEmpty(VehicleMake.logo))
                            {
                                DeleteMakeLogo(VehicleMake.logo);
                            }

                            string fileName;
                            success = UploadVehicleMakeLogo(edit.logo, success, out fileName);
                            if (success)
                            {
                                VehicleMake.name = edit.name;
                                VehicleMake.logo = fileName;
                            }
                        }
                        else
                        {
                            VehicleMake.name = edit.name;
                        }

                        _vehicleMakeRepository.Update();
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

        public bool DeleteVehicleMake(VehicleMakeDeleteModel model)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            try
            {
                make VehicleMake;
                if (GetVehicleMake(new VehicleMakeGetModel { makeid = model.makeid }, out VehicleMake))
                {
                    if (!String.IsNullOrEmpty(VehicleMake.logo))
                    {
                        if (DeleteMakeLogo(VehicleMake.logo))
                        {
                            _vehicleMakeRepository.DeleteVehicleMake(VehicleMake);
                        }
                    }
                    else
                    {
                        _vehicleMakeRepository.DeleteVehicleMake(VehicleMake);
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

        public bool VehicleMakeUp(int? Id)
        {
            bool success = true;
            try
            {
                make vehicleMakeSwap = _vehicleMakeRepository.GetVehicleMake(Id.Value);
                make vehicleMakeSwapWith = _vehicleMakeRepository.GetVehicleMakeAbove(Id.Value);
                int tempOrder = vehicleMakeSwap.sortorder;
                vehicleMakeSwap.sortorder = vehicleMakeSwapWith.sortorder;
                vehicleMakeSwapWith.sortorder = tempOrder;
                _vehicleMakeRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }

        public bool VehicleMakeDown(int? Id)
        {
            bool success = true;
            try
            {
                make vehicleMakeSwap = _vehicleMakeRepository.GetVehicleMake(Id.Value);
                make vehicleMakeSwapWith = _vehicleMakeRepository.GetVehicleMakeBelow(Id.Value);
                int tempOrder = vehicleMakeSwap.sortorder;
                vehicleMakeSwap.sortorder = vehicleMakeSwapWith.sortorder;
                vehicleMakeSwapWith.sortorder = tempOrder;
                _vehicleMakeRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }
        
    }
}