using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MotorMart.Core.Models
{
    public class LinqApplicationSettingRepository : ILinqApplicationSettingRepository
    {
        private MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();

        public string GetApplicationSetting(string settingName)
        {
            
            string result = String.Empty;
            try
            {
                applicationsetting thisSetting = _datacontext.applicationsettings.Where(a => a.name.ToLower() == settingName.ToLower()).FirstOrDefault();
                result = thisSetting.value;
            }
            catch(Exception ex)
            {
                throw new Exception("Error", ex);
            }
            _datacontext.Connection.Close();
            if (result == null) result = String.Empty;
            return result;
        }

        public applicationsetting GetApplicationSetting(int applicationSettingId)
        {
            return _datacontext.applicationsettings.Where(a => a.applicationsettingid == applicationSettingId).FirstOrDefault();
        }

        public applicationsetting GetApplicationSettingByName(string settingName)
        {
            return _datacontext.applicationsettings.Where(a => a.name.ToLower() == settingName.ToLower()).FirstOrDefault();
        }

        public bool ApplicationSettingExists(string name)
        {
            return _datacontext.applicationsettings.Where(a => a.name.ToLower() == name.ToLower()).Any();
        }

        public List<applicationsetting> GetApplicationSettings()
        {
            return _datacontext.applicationsettings.ToList();
        }

        public void AddApplicationSetting(applicationsetting SettingToAdd)
        {
            _datacontext.applicationsettings.InsertOnSubmit(SettingToAdd);
            _datacontext.SubmitChanges();
        }

        public void DeleteApplicationSetting(applicationsetting SettingToDelete)
        {
            _datacontext.applicationsettings.DeleteOnSubmit(SettingToDelete);
            _datacontext.SubmitChanges();
        }

        public void Update()
        {
            _datacontext.SubmitChanges();
        }
    }
}
