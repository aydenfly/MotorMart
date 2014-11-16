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
    public class TransmissionService : ITransmissionService
    {
        private IValidationDictionary _validationDictionary;
        private ILinqVehicleRepository _vehicleRepository;
        private ILinqTransmissionRepository _transmissionRepository;

        public TransmissionService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new LinqVehicleRepository(), new LinqTransmissionRepository())
        {
        }

        public TransmissionService(IValidationDictionary validationDictionary, LinqVehicleRepository vehicleRepository, LinqTransmissionRepository bodyTypeRepository)
        {
            _validationDictionary = validationDictionary;
            _transmissionRepository = bodyTypeRepository;
            _vehicleRepository = vehicleRepository;
        }

        #region Helpers

        public bool GetTransmission(TransmissionGetModel get, out transmission Transmission)
        {
            bool success = false;
            Transmission = _transmissionRepository.GetTransmission(get.transmissionid);
            if (Transmission != null)
            {
                success = true;
            }
            return success;
        }

        private bool TransmissionAlreadyExists(string name)
        {
            return _transmissionRepository.TransmissionExists(name.Trim());
        }

        private bool TransmissionAlreadyExists(int transmissionId, string name)
        {
            bool exists = false;
            if (_transmissionRepository.GetTransmission(name.Trim()) != null)
            {
                if (transmissionId != _transmissionRepository.GetTransmission(name.Trim()).transmissionid)
                {
                    exists = true;
                }
            }
            return exists;
        }


        #endregion

        #region IVehicle Model service members

        public void PopulateTransmissionViewModel(TransmissionViewModel model)
        {
            try
            {
                if (model == null) model = new TransmissionViewModel();

                if (model.get != null)
                {
                    transmission Transmission;
                    if (GetTransmission(model.get, out Transmission))
                    {
                        model.CurrentTransmission = Transmission;

                        if (model.add == null) model.add = PopulateTransmissionAddModel(Transmission);
                        if (model.edit == null) model.edit = PopulateTransmissionEditModel(Transmission);
                        model.delete = new TransmissionDeleteModel { transmissionid = Transmission.transmissionid, CurrentTransmission = Transmission };
                    }
                    else
                    {
                        if (model.add == null) model.add = PopulateTransmissionAddModel(new transmission());
                        if (model.edit == null) model.edit = PopulateTransmissionEditModel(new transmission());
                    }
                }
                
                model.TransmissionsList = _transmissionRepository.GetTransmissions();

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        #endregion

        #region Models

        public TransmissionAddModel PopulateTransmissionAddModel(transmission Model)
        {
            TransmissionAddModel transmission = new TransmissionAddModel
            {
            };
            return transmission;
        }

        public TransmissionEditModel PopulateTransmissionEditModel(transmission Model)
        {
            TransmissionEditModel transmission = new TransmissionEditModel
            {
                transmissionid = Model.transmissionid,
                name = Model.name,

            };
            return transmission;
        }

        #endregion

        public bool AddTransmission(TransmissionAddModel add)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            if (TransmissionAlreadyExists(add.name))
            {
                _validationDictionary.AddError("Error", "The transmission supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {
                try
                {
                    var AvailableTransmissions = _transmissionRepository.GetTransmissions().ToList();
                    int SortOrder = AvailableTransmissions.Count > 0 ? AvailableTransmissions.OrderByDescending(v => v.sortorder).FirstOrDefault().sortorder + 1 : 0;

                    add.NewTransmission = new transmission
                    {
                        name = add.name,
                        sortorder = SortOrder
                    };

                    _transmissionRepository.AddTransmission(add.NewTransmission);
                    success = true;

                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            return success;
        }

        public bool EditTransmission(TransmissionEditModel edit)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            if (TransmissionAlreadyExists(edit.transmissionid, edit.name))
            {
                _validationDictionary.AddError("Error", "The transmission supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {
                try
                {
                    transmission Transmission;
                    if (GetTransmission(new TransmissionGetModel { transmissionid = edit.transmissionid }, out Transmission))
                    {
                        Transmission.transmissionid = edit.transmissionid;
                        Transmission.name = edit.name;

                        _transmissionRepository.Update();
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

        public bool DeleteTransmission(TransmissionDeleteModel model)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            try
            {
                transmission Transmission;
                if (GetTransmission(new TransmissionGetModel { transmissionid = model.transmissionid }, out Transmission))
                {
                    _transmissionRepository.DeleteTransmission(Transmission);
                }
                success = _validationDictionary.IsValid;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;

        }

        public bool TransmissionUp(int? Id)
        {
            bool success = true;
            try
            {
                transmission transmissionSwap = _transmissionRepository.GetTransmission(Id.Value);
                transmission transmissionSwapWith = _transmissionRepository.GetTransmissionAbove(Id.Value);
                int tempOrder = transmissionSwap.sortorder;
                transmissionSwap.sortorder = transmissionSwapWith.sortorder;
                transmissionSwapWith.sortorder = tempOrder;
                _transmissionRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }

        public bool TransmissionDown(int? Id)
        {
            bool success = true;
            try
            {
                transmission transmissionSwap = _transmissionRepository.GetTransmission(Id.Value);
                transmission transmissionSwapWith = _transmissionRepository.GetTransmissionBelow(Id.Value);
                int tempOrder = transmissionSwap.sortorder;
                transmissionSwap.sortorder = transmissionSwapWith.sortorder;
                transmissionSwapWith.sortorder = tempOrder;
                _transmissionRepository.Update();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }
        
    }
}