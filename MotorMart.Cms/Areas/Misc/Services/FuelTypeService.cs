using System;
using System.Linq;
using MotorMart.Core.Models.Validation;
using MotorMart.Core.Models;
using MotorMart.Cms.Models;
using MotorMart.Cms.Areas.Misc.Models;
using Elmah;
using MotorMart.Core.HtmlHelpers;


namespace MotorMart.Cms.Areas.Misc.Services
{
    public class FuelTypeService : IFuelTypeService
    {
        private IValidationDictionary _validationDictionary;
        private ILinqVehicleRepository _vehicleRepository;
        private ILinqFuelTypeRepository _fuelTypeRepository;

        public FuelTypeService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new LinqVehicleRepository(), new LinqFuelTypeRepository())
        {
        }

        public FuelTypeService(IValidationDictionary validationDictionary, LinqVehicleRepository vehicleRepository, LinqFuelTypeRepository bodyTypeRepository)
        {
            _validationDictionary = validationDictionary;
            _fuelTypeRepository = bodyTypeRepository;
            _vehicleRepository = vehicleRepository;
        }

        #region Helpers

        public bool GetFuelType(FuelTypeGetModel get, out fueltype FuelType)
        {
            bool success = false;
            FuelType = _fuelTypeRepository.GetFuelType(get.fueltypeid);
            if (FuelType != null)
            {
                success = true;
            }
            return success;
        }

        private bool FuelTypeAlreadyExists(string type)
        {
            return _fuelTypeRepository.FuelTypeExists(type.Trim());
        }

        private bool FuelTypeAlreadyExists(int fuelTypeId, string type)
        {
            bool exists = false;
            if (_fuelTypeRepository.GetFuelType(type.Trim()) != null)
            {
                if (fuelTypeId != _fuelTypeRepository.GetFuelType(type.Trim()).fueltypeid)
                {
                    exists = true;
                }
            }
            return exists;
        }

        #endregion

        #region IVehicle Model service members

        public void PopulateFuelTypeViewModel(FuelTypeViewModel model)
        {
            try
            {
                if (model == null) model = new FuelTypeViewModel();

                if (model.get != null)
                {
                    fueltype FuelType;
                    if (GetFuelType(model.get, out FuelType))
                    {
                        model.CurrentFuelType = FuelType;

                        if (model.add == null) model.add = PopulateFuelTypeAddModel(FuelType);
                        if (model.edit == null) model.edit = PopulateFuelTypeEditModel(FuelType);
                        model.delete = new FuelTypeDeleteModel { fueltypeid = FuelType.fueltypeid, CurrentFuelType = FuelType };
                    }
                    else
                    {
                        if (model.add == null) model.add = PopulateFuelTypeAddModel(new fueltype());
                        if (model.edit == null) model.edit = PopulateFuelTypeEditModel(new fueltype());
                    }
                }
                
                model.FuelTypesList = _fuelTypeRepository.GetFuelTypes();

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        #endregion

        #region Models

        public FuelTypeAddModel PopulateFuelTypeAddModel(fueltype Model)
        {
            FuelTypeAddModel fueltype = new FuelTypeAddModel
            {
            };
            return fueltype;
        }

        public FuelTypeEditModel PopulateFuelTypeEditModel(fueltype Model)
        {
            FuelTypeEditModel fueltype = new FuelTypeEditModel
            {
                fueltypeid = Model.fueltypeid,
                type = Model.type,

            };
            return fueltype;
        }

        #endregion

        public bool AddFuelType(FuelTypeAddModel add)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            
            if (FuelTypeAlreadyExists(add.type))
            {
                _validationDictionary.AddError("Error", "The fuel type supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {

                try
                {
                    var AvailableFuelTypes = _fuelTypeRepository.GetFuelTypes().ToList();
                    int SortOrder = AvailableFuelTypes.Count > 0 ? AvailableFuelTypes.OrderByDescending(v => v.sortorder).FirstOrDefault().sortorder + 1 : 0;

                    add.NewFuelType = new fueltype
                    {
                        type = add.type,
                        sortorder = SortOrder
                    };

                    _fuelTypeRepository.AddFuelType(add.NewFuelType);
                    success = true;

                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            return success;
        }

        public bool EditFuelType(FuelTypeEditModel edit)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            if (FuelTypeAlreadyExists(edit.fueltypeid, edit.type))
            {
                _validationDictionary.AddError("Error", "The fuel type supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {
                try
                {
                    fueltype Model;
                    if (GetFuelType(new FuelTypeGetModel { fueltypeid = edit.fueltypeid }, out Model))
                    {
                        Model.fueltypeid = edit.fueltypeid;
                        Model.type = edit.type;

                        _fuelTypeRepository.Update();
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

        public bool DeleteFuelType(FuelTypeDeleteModel model)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            try
            {
                fueltype Model;
                if (GetFuelType(new FuelTypeGetModel { fueltypeid = model.fueltypeid }, out Model))
                {
                    _fuelTypeRepository.DeleteFuelType(Model);
                }
                success = _validationDictionary.IsValid;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;

        }

        public bool FuelTypeUp(int? Id)
        {
            bool success = true;
            try
            {
                fueltype fuelTypeSwap = _fuelTypeRepository.GetFuelType(Id.Value);
                fueltype fuelTypeSwapWith = _fuelTypeRepository.GetFuelTypeAbove(Id.Value);
                int tempOrder = fuelTypeSwap.sortorder;
                fuelTypeSwap.sortorder = fuelTypeSwapWith.sortorder;
                fuelTypeSwapWith.sortorder = tempOrder;
                _fuelTypeRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }

        public bool FuelTypeDown(int? Id)
        {
            bool success = true;
            try
            {
                fueltype fuelTypeSwap = _fuelTypeRepository.GetFuelType(Id.Value);
                fueltype fuelTypeSwapWith = _fuelTypeRepository.GetFuelTypeBelow(Id.Value);
                int tempOrder = fuelTypeSwap.sortorder;
                fuelTypeSwap.sortorder = fuelTypeSwapWith.sortorder;
                fuelTypeSwapWith.sortorder = tempOrder;
                _fuelTypeRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }
        
    }
}