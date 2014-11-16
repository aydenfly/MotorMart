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
    public class VehicleColorService : IVehicleColorService
    {
        private IValidationDictionary _validationDictionary;
        private ILinqVehicleRepository _vehicleRepository;
        private ILinqVehicleColorRepository _vehicleColorRepository;

        public VehicleColorService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new LinqVehicleRepository(), new LinqVehicleColorRepository())
        {
        }

        public VehicleColorService(IValidationDictionary validationDictionary, LinqVehicleRepository vehicleRepository, LinqVehicleColorRepository vehicleColorRepository)
        {
            _validationDictionary = validationDictionary;
            _vehicleColorRepository = vehicleColorRepository;
            _vehicleRepository = vehicleRepository;
        }

        #region Helpers

        public bool GetVehicleColor(VehicleColorGetModel get, out color Color)
        {
            bool success = false;
            Color = _vehicleColorRepository.GetVehicleColor(get.colorid);
            if (Color != null)
            {
                success = true;
            }
            return success;
        }

        private bool ColorAlreadyExists(string name)
        {
            return _vehicleColorRepository.ColorExists(name.Trim());
        }

        private bool ColorAlreadyExists(int colorid, string name)
        {
            bool exists = false;
            if (_vehicleColorRepository.GetVehicleColor(name.Trim()) != null)
            {
                if (colorid != _vehicleColorRepository.GetVehicleColor(name.Trim()).colorid)
                {
                    exists = true;
                }
            }
            return exists;
        }

        #endregion

        #region IVehicle Model service members

        public void PopulateVehicleColorViewModel(VehicleColorViewModel model)
        {
            try
            {
                if (model == null) model = new VehicleColorViewModel();

                if (model.get != null)
                {
                    color Color;
                    if (GetVehicleColor(model.get, out Color))
                    {
                        model.CurrentColor = Color;

                        if (model.add == null) model.add = PopulateVehicleColorAddModel(Color);
                        if (model.edit == null) model.edit = PopulateVehicleColorEditModel(Color);
                        model.delete = new VehicleColorDeleteModel { colorid = Color.colorid, CurrentColor = Color };
                    }
                    else
                    {
                        if (model.add == null) model.add = PopulateVehicleColorAddModel(new color());
                        if (model.edit == null) model.edit = PopulateVehicleColorEditModel(new color());
                    }
                }
                
                model.ColorsList = _vehicleColorRepository.GetVehicleColors();

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        #region Models

        public VehicleColorAddModel PopulateVehicleColorAddModel(color Model)
        {
            VehicleColorAddModel color = new VehicleColorAddModel
            {
            };
            return color;
        }

        public VehicleColorEditModel PopulateVehicleColorEditModel(color Model)
        {
            VehicleColorEditModel color = new VehicleColorEditModel
            {
                colorid = Model.colorid,
                name = Model.name,

            };
            return color;
        }

        #endregion

        public bool AddVehicleColor(VehicleColorAddModel add)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            if (ColorAlreadyExists(add.name))
            {
                _validationDictionary.AddError("Error", "The color supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {
                try
                {
                    var AvailableColors = _vehicleColorRepository.GetVehicleColors().ToList();
                    int SortOrder = AvailableColors.Count > 0 ? AvailableColors.OrderByDescending(v => v.sortorder).FirstOrDefault().sortorder + 1 : 0;

                    add.NewVehicleColor = new color
                    {
                        name = add.name,
                        sortorder = SortOrder
                    };

                    _vehicleColorRepository.AddVehicleColor(add.NewVehicleColor);
                    success = true;

                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                    _validationDictionary.AddError("Error", ex.ToString());
                }
            }
            return success;
        }

        public bool EditVehicleColor(VehicleColorEditModel edit)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            
            if (ColorAlreadyExists(edit.colorid, edit.name))
            {
                _validationDictionary.AddError("Error", "The color supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {
                try
                {
                    color Color;
                    if (GetVehicleColor(new VehicleColorGetModel { colorid = edit.colorid }, out Color))
                    {
                        Color.colorid = edit.colorid;
                        Color.name = edit.name;

                        _vehicleColorRepository.Update();
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

        public bool DeleteVehicleColor(VehicleColorDeleteModel model)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            try
            {
                color Color;
                if (GetVehicleColor(new VehicleColorGetModel { colorid = model.colorid }, out Color))
                {
                    _vehicleColorRepository.DeleteVehicleColor(Color);
                }
                success = _validationDictionary.IsValid;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;

        }

        public bool VehicleColorUp(int? Id)
        {
            bool success = true;
            try
            {
                color colorSwap = _vehicleColorRepository.GetVehicleColor(Id.Value);
                color colorSwapWith = _vehicleColorRepository.GetVehicleColorAbove(Id.Value);
                int tempOrder = colorSwap.sortorder;
                colorSwap.sortorder = colorSwapWith.sortorder;
                colorSwapWith.sortorder = tempOrder;
                _vehicleColorRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }

        public bool VehicleColorDown(int? Id)
        {
            bool success = true;
            try
            {
                color colorSwap = _vehicleColorRepository.GetVehicleColor(Id.Value);
                color colorSwapWith = _vehicleColorRepository.GetVehicleColorBelow(Id.Value);
                int tempOrder = colorSwap.sortorder;
                colorSwap.sortorder = colorSwapWith.sortorder;
                colorSwapWith.sortorder = tempOrder;
                _vehicleColorRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }

        #endregion
    }
}