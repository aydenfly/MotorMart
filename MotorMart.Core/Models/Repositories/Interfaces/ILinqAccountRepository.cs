using System.Collections.Generic;

namespace MotorMart.Core.Models
{
    public interface ILinqAccountRepository
    {
        void AddUserAccount(useraccount UserAccountToAdd);

        void AddUserGroup(usergroup UserGroupToAdd);

        useraccount GetUserAccount(int p);

        useraccount GetUserAccount(int UserAccountId, string SecurityKey);

        useraccount GetUserAccount(string username);

        useraccount GetUserAccount(LoginModel model);

        List<useraccount> GetUserAccountList();

        usergroup GetUserGroup(int p);

        List<usergroup> GetUserGroupList();

        void Update();
    }
}
