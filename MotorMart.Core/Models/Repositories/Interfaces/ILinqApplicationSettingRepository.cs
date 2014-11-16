using System;
using MotorMart.Core.Models;
using System.Collections.Generic;

namespace MotorMart.Core.Models
{
    public interface ILinqApplicationSettingRepository
    {
        string GetApplicationSetting(string settingName);

        applicationsetting GetApplicationSetting(int applicationSettingId);

        applicationsetting GetApplicationSettingByName(string settingName);

        bool ApplicationSettingExists(string name);

        List<applicationsetting> GetApplicationSettings();

        void AddApplicationSetting(applicationsetting SettingToAdd);

        void DeleteApplicationSetting(applicationsetting SettingToDelete);

        void Update();
    }
}
