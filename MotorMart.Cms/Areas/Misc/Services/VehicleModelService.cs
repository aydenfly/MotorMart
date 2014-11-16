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
    public class VehicleModelService : IVehicleModelService
    {
        private IValidationDictionary _validationDictionary;
        private ILinqVehicleRepository _vehicleRepository;
        private IVehicleModelRepository _vehicleModelRepository;

        public VehicleModelService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new LinqVehicleRepository(), new LinqVehicleModelRepository())
        {
        }

        public VehicleModelService(IValidationDictionary validationDictionary, LinqVehicleRepository vehicleRepository, LinqVehicleModelRepository vehicleModelRepository)
        {
            _validationDictionary = validationDictionary;
            _vehicleModelRepository = vehicleModelRepository;
            _vehicleRepository = vehicleRepository;
        }

        #region Helpers

        public bool GetVehicleModel(VehicleModelGetModel get, out model VehicleModel)
        {
            bool success = false;
            VehicleModel = _vehicleModelRepository.GetVehicleModel(get.modelid);
            if (VehicleModel != null)
            {
                success = true;
            }
            return success;
        }

        private bool VehicleModelAlreadyExists(int MakeId, string modelName)
        {
            return _vehicleModelRepository.VehicleModelExists(MakeId, modelName.Trim());
        }

        private bool VehicleModelAlreadyExists(int MakeId, int ModelId, string modelName)
        {
            bool exists = false;
            if (_vehicleModelRepository.GetVehicleModel(MakeId, modelName.Trim()) != null)
            {
                if (ModelId != _vehicleModelRepository.GetVehicleModel(MakeId, modelName.Trim()).modelid)
                {
                    exists = true;
                }
            }
            return exists;
        }

        #endregion

        #region IVehicle Model service members

        public void PopulateVehicleModelViewModel(VehicleModelViewModel model)
        {
            try
            {
                if (model == null) model = new VehicleModelViewModel();

                if (model.get != null)
                {
                    model Model;
                    if (GetVehicleModel(model.get, out Model))
                    {
                        model.CurrentModel = Model;

                        #region Select list

                        model.MakeSelect = SelectListHelper.GetVehicleMakeSelect(_vehicleRepository.GetVehicleMakeList(), Model.makeid);

                        #endregion

                        if (model.add == null) model.add = PopulateVehicleModelAddModel(Model);
                        if (model.edit == null) model.edit = PopulateVehicleModelEditModel(Model);
                        model.delete = new VehicleModelDeleteModel { makeid = Model.makeid, modelid = Model.modelid, CurrentModel = Model };
                    }
                    else
                    {
                        if (model.add == null) model.add = PopulateVehicleModelAddModel(new model());
                        if (model.edit == null) model.edit = PopulateVehicleModelEditModel(new model());
                    }
                }
                
                model.MakeSelect = SelectListHelper.GetVehicleMakeSelect(_vehicleRepository.GetVehicleMakeList(), 0);

                model.ModelsList = _vehicleModelRepository.GetVehicleModels();
                model.MakesList = _vehicleRepository.GetVehicleMakeList();

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        #endregion

        #region Models

        public VehicleModelAddModel PopulateVehicleModelAddModel(model Model)
        {
            VehicleModelAddModel model = new VehicleModelAddModel
            {
            };
            return model;
        }

        public VehicleModelEditModel PopulateVehicleModelEditModel(model Model)
        {
            VehicleModelEditModel model = new VehicleModelEditModel
            {
                makeid = Model.makeid,
                modelid = Model.modelid,
                name = Model.name,

            };
            return model;
        }

        #endregion

        public bool AddVehicleModel(VehicleModelAddModel add)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            if (VehicleModelAlreadyExists(add.makeid, add.name))
            {
                _validationDictionary.AddError("Error", "The vehicle model supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {

                try
                {
                    var AvailableMakeVehicleModels = _vehicleModelRepository.GetVehicleModels().Where(mk=>mk.makeid == add.makeid).ToList();
                    int SortOrder = AvailableMakeVehicleModels.Count > 0 ? AvailableMakeVehicleModels.OrderByDescending(v => v.sortorder).FirstOrDefault().sortorder + 1 : 0;

                    add.NewModel = new model
                    {
                        makeid = add.makeid,
                        name = add.name,
                        sortorder = SortOrder
                    };

                    _vehicleModelRepository.AddVehicleModel(add.NewModel);
                    success = true;

                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            return success;
        }

        public bool EditVehicleModel(VehicleModelEditModel edit)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;


            if (VehicleModelAlreadyExists(edit.makeid, edit.modelid, edit.name))
            {
                _validationDictionary.AddError("Error", "The color supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {
                try
                {
                    model Model;
                    if (GetVehicleModel(new VehicleModelGetModel { modelid = edit.modelid }, out Model))
                    {
                        Model.makeid = edit.makeid;
                        Model.modelid = edit.modelid;
                        Model.name = edit.name;

                        _vehicleModelRepository.Update();
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

        public bool DeleteVehicleModel(VehicleModelDeleteModel model)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            try
            {
                model Model;
                if (GetVehicleModel(new VehicleModelGetModel { modelid = model.modelid }, out Model))
                {
                    _vehicleModelRepository.DeleteVehicleModel(Model);
                }
                success = _validationDictionary.IsValid;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;

        }

        public bool VehicleModelUp(int? Id)
        {
            bool success = true;
            try
            {
                model modelSwap = _vehicleModelRepository.GetVehicleModel(Id.Value);
                model modelSwapWith = _vehicleModelRepository.GetVehicleModelAbove(Id.Value);
                int tempOrder = modelSwap.sortorder;
                modelSwap.sortorder = modelSwapWith.sortorder;
                modelSwapWith.sortorder = tempOrder;
                _vehicleModelRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }

        public bool VehicleModelDown(int? Id)
        {
            bool success = true;
            try
            {
                model modelSwap = _vehicleModelRepository.GetVehicleModel(Id.Value);
                model modelSwapWith = _vehicleModelRepository.GetVehicleModelBelow(Id.Value);
                int tempOrder = modelSwap.sortorder;
                modelSwap.sortorder = modelSwapWith.sortorder;
                modelSwapWith.sortorder = tempOrder;
                _vehicleModelRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }
        
    }
}