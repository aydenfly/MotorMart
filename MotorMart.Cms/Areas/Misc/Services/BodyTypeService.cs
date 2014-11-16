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
    public class BodyTypeService : IBodyTypeService
    {
        private IValidationDictionary _validationDictionary;
        private ILinqVehicleRepository _vehicleRepository;
        private ILinqBodyTypeRepository _bodyTypeRepository;

        public BodyTypeService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new LinqVehicleRepository(), new LinqBodyTypeRepository())
        {
        }

        public BodyTypeService(IValidationDictionary validationDictionary, LinqVehicleRepository vehicleRepository, LinqBodyTypeRepository bodyTypeRepository)
        {
            _validationDictionary = validationDictionary;
            _bodyTypeRepository = bodyTypeRepository;
            _vehicleRepository = vehicleRepository;
        }

        #region Helpers

        public bool GetBodyType(BodyTypeGetModel get, out bodytype BodyType)
        {
            bool success = false;
            BodyType = _bodyTypeRepository.GetBodyType(get.bodytypeid);
            if (BodyType != null)
            {
                success = true;
            }
            return success;
        }

        private bool BodyTypeAlreadyExists(string type)
        {
            return _bodyTypeRepository.BodyTypeExists(type.Trim());
        }

        private bool BodyTypeAlreadyExists(int bodyTypeId, string type)
        {
            bool exists = false;
            if (_bodyTypeRepository.GetBodyType(type.Trim()) != null)
            {
                if (bodyTypeId != _bodyTypeRepository.GetBodyType(type.Trim()).bodytypeid)
                {
                    exists = true;
                }
            }
            return exists;
        }

        #endregion

        #region IBodytype service members

        public void PopulateBodyTypeViewModel(BodyTypeViewModel model)
        {
            try
            {
                if (model == null) model = new BodyTypeViewModel();

                if (model.get != null)
                {
                    bodytype BodyType;
                    if (GetBodyType(model.get, out BodyType))
                    {
                        model.CurrentBodyType = BodyType;                        

                        if (model.add == null) model.add = PopulateBodyTypeAddModel(BodyType);
                        if (model.edit == null) model.edit = PopulateBodyTypeEditModel(BodyType);
                        model.delete = new BodyTypeDeleteModel { bodytypeid = BodyType.bodytypeid, CurrentBodyType = BodyType };
                    }
                    else
                    {
                        if (model.add == null) model.add = PopulateBodyTypeAddModel(new bodytype());
                        if (model.edit == null) model.edit = PopulateBodyTypeEditModel(new bodytype());
                    }
                }
                
                model.BodyTypesList = _bodyTypeRepository.GetBodyTypes();

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        #endregion

        #region Models

        public BodyTypeAddModel PopulateBodyTypeAddModel(bodytype BodyType)
        {
            BodyTypeAddModel model = new BodyTypeAddModel
            {
            };
            return model;
        }

        public BodyTypeEditModel PopulateBodyTypeEditModel(bodytype BodyType)
        {
            BodyTypeEditModel model = new BodyTypeEditModel
            {
                bodytypeid = BodyType.bodytypeid,
                type = BodyType.type,

            };
            return model;
        }

        #endregion

        public bool AddBodyType(BodyTypeAddModel add)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            if (BodyTypeAlreadyExists(add.type))
            {
                _validationDictionary.AddError("Error", "The body type supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {

                try
                {
                    var AvailableBodyTypes = _bodyTypeRepository.GetBodyTypes().ToList();
                    int SortOrder = AvailableBodyTypes.Count > 0 ? AvailableBodyTypes.OrderByDescending(v => v.sortorder).FirstOrDefault().sortorder + 1 : 0;

                    add.NewBodyType = new bodytype
                    {
                        type = add.type,
                        sortorder = SortOrder
                    };

                    _bodyTypeRepository.AddBodyType(add.NewBodyType);
                    success = true;

                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            return success;
        }

        public bool EditBodyType(BodyTypeEditModel edit)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            if (BodyTypeAlreadyExists(edit.bodytypeid, edit.type))
            {
                _validationDictionary.AddError("Error", "The body type supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {

                try
                {
                    bodytype BodyType;
                    if (GetBodyType(new BodyTypeGetModel { bodytypeid = edit.bodytypeid }, out BodyType))
                    {
                        BodyType.bodytypeid = edit.bodytypeid;
                        BodyType.type = edit.type;

                        _bodyTypeRepository.Update();
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

        public bool DeleteBodyType(BodyTypeDeleteModel model)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            try
            {
                bodytype BodyType;
                if (GetBodyType(new BodyTypeGetModel { bodytypeid = model.bodytypeid }, out BodyType))
                {
                    _bodyTypeRepository.DeleteBodyType(BodyType);
                }
                success = _validationDictionary.IsValid;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;

        }

        public bool BodyTypeUp(int? Id)
        {
            bool success = true;
            try
            {
                bodytype bodyTypeSwap = _bodyTypeRepository.GetBodyType(Id.Value);
                bodytype bodyTypeSwapWith = _bodyTypeRepository.GetBodyTypeAbove(Id.Value);
                int tempOrder = bodyTypeSwap.sortorder;
                bodyTypeSwap.sortorder = bodyTypeSwapWith.sortorder;
                bodyTypeSwapWith.sortorder = tempOrder;
                _bodyTypeRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }

        public bool BodyTypeDown(int? Id)
        {
            bool success = true;
            try
            {
                bodytype bodyTypeSwap = _bodyTypeRepository.GetBodyType(Id.Value);
                bodytype bodyTypeSwapWith = _bodyTypeRepository.GetBodyTypeBelow(Id.Value);
                int tempOrder = bodyTypeSwap.sortorder;
                bodyTypeSwap.sortorder = bodyTypeSwapWith.sortorder;
                bodyTypeSwapWith.sortorder = tempOrder;
                _bodyTypeRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }
        
    }
}