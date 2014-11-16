using System;
using MotorMart.Core.Models;
using MotorMart.Cms.Areas.Misc.Models;

namespace MotorMart.Cms.Areas.Misc.Services
{
    public interface IApplicationSettingService
    {
        bool AddApplicationSetting(ApplicationSettingAddModel add);

        bool DeleteApplicationSetting(ApplicationSettingDeleteModel model);

        bool EditApplicationSetting(ApplicationSettingEditModel edit);

        bool GetApplicationSetting(ApplicationSettingGetModel get, out applicationsetting ApplicationSetting);

        ApplicationSettingAddModel PopulateApplicationSettingAddModel(applicationsetting ApplicationSetting);

        ApplicationSettingEditModel PopulateApplicationSettingEditModel(applicationsetting ApplicationSetting);

        void PopulateApplicationSettingViewModel(ApplicationSettingViewModel model);
    }
}
