using MotorMart.Core.Models;
using MotorMart.Cms.Models;
using MotorMart.Core.Common;
using System.Collections.Generic;
using MotorMart.Cms.Areas.Account.Models;
using MotorMart.Cms.Areas.Misc.Models;

namespace MotorMart.Cms.Areas.Account.Services
{
    public interface IAccountService
    {
        bool AddUserAccount(UserAccountAddModel add);

        bool EditUserAccount(UserAccountEditModel edit);

        useraccount GetUserAccount(int UserAccountId, string SecurityKey);

        useraccount GetUserAccount(int? Id);

        List<useraccount> GetUserAccountList();

        usergroup GetUserGroup(int? Id);

        List<usergroup> GetUserGroupList();

        bool LoginUser(LoginModel model, IAppCookies iAppCookies);

        void LogoutUser(IAppCookies iAppCookies);
    }
}
