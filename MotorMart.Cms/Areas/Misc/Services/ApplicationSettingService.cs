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
    public class ApplicationSettingService : IApplicationSettingService
    {
        private IValidationDictionary _validationDictionary;
        private ILinqApplicationSettingRepository _applicationSettingRepository;

        public ApplicationSettingService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new LinqApplicationSettingRepository())
        {
        }

        public ApplicationSettingService(IValidationDictionary validationDictionary, LinqApplicationSettingRepository applicationSettingRepository)
        {
            _validationDictionary = validationDictionary;
            _applicationSettingRepository = applicationSettingRepository;
        }

        #region Helpers

        public bool GetApplicationSetting(ApplicationSettingGetModel get, out applicationsetting ApplicationSetting)
        {
            bool success = false;
            ApplicationSetting = _applicationSettingRepository.GetApplicationSetting(get.applicationsettingid);
            if (ApplicationSetting != null)
            {
                success = true;
            }
            return success;
        }

        private bool ApplicationSettingAlreadyExists(string type)
        {
            return _applicationSettingRepository.ApplicationSettingExists(type.Trim());
        }

        private bool ApplicationSettingAlreadyExists(int applicationSettingId, string name)
        {
            bool exists = false;
            if (_applicationSettingRepository.GetApplicationSettingByName(name.Trim()) != null)
            {
                if (applicationSettingId != _applicationSettingRepository.GetApplicationSettingByName(name.Trim()).applicationsettingid)
                {
                    exists = true;
                }
            }
            return exists;
        }

        #endregion

        #region IApplicationSetting service members

        public void PopulateApplicationSettingViewModel(ApplicationSettingViewModel model)
        {
            try
            {
                if (model == null) model = new ApplicationSettingViewModel();

                if (model.get != null)
                {
                    applicationsetting ApplicationSetting;
                    if (GetApplicationSetting(model.get, out ApplicationSetting))
                    {
                        model.CurrentApplicationSetting = ApplicationSetting;                        

                        if (model.add == null) model.add = PopulateApplicationSettingAddModel(ApplicationSetting);
                        if (model.edit == null) model.edit = PopulateApplicationSettingEditModel(ApplicationSetting);
                        model.delete = new ApplicationSettingDeleteModel { applicationsettingid = ApplicationSetting.applicationsettingid, CurrentApplicationSetting = ApplicationSetting };
                    }
                    else
                    {
                        if (model.add == null) model.add = PopulateApplicationSettingAddModel(new applicationsetting());
                        if (model.edit == null) model.edit = PopulateApplicationSettingEditModel(new applicationsetting());
                    }
                }
                
                model.ApplicationSettingsList = _applicationSettingRepository.GetApplicationSettings();

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        #endregion

        #region Models

        public ApplicationSettingAddModel PopulateApplicationSettingAddModel(applicationsetting ApplicationSetting)
        {
            ApplicationSettingAddModel model = new ApplicationSettingAddModel
            {
            };
            return model;
        }

        public ApplicationSettingEditModel PopulateApplicationSettingEditModel(applicationsetting ApplicationSetting)
        {
            ApplicationSettingEditModel model = new ApplicationSettingEditModel
            {
                applicationsettingid = ApplicationSetting.applicationsettingid,
                name = ApplicationSetting.name,
                value = ApplicationSetting.value
            };
            return model;
        }

        #endregion

        public bool AddApplicationSetting(ApplicationSettingAddModel add)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            if (ApplicationSettingAlreadyExists(add.name))
            {
                _validationDictionary.AddError("Error", "The setting name supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {

                try
                {
                    add.NewApplicationSetting = new applicationsetting
                    {
                        name = add.name,
                        value = add.value
                    };

                    _applicationSettingRepository.AddApplicationSetting(add.NewApplicationSetting);
                    success = true;

                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            return success;
        }

        public bool EditApplicationSetting(ApplicationSettingEditModel edit)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;

            if (ApplicationSettingAlreadyExists(edit.applicationsettingid, edit.name))
            {
                _validationDictionary.AddError("Error", "The setting name supplied already exists!");
            }

            if (_validationDictionary.IsValid)
            {

                try
                {
                    applicationsetting ApplicationSetting;
                    if (GetApplicationSetting(new ApplicationSettingGetModel { applicationsettingid = edit.applicationsettingid }, out ApplicationSetting))
                    {
                        ApplicationSetting.name = edit.name;
                        ApplicationSetting.value = edit.value;
                        _applicationSettingRepository.Update();
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

        public bool DeleteApplicationSetting(ApplicationSettingDeleteModel model)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            try
            {
                applicationsetting ApplicationSetting;
                if (GetApplicationSetting(new ApplicationSettingGetModel { applicationsettingid = model.applicationsettingid }, out ApplicationSetting))
                {
                    _applicationSettingRepository.DeleteApplicationSetting(ApplicationSetting);
                }
                success = _validationDictionary.IsValid;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;

        }       
    }
}