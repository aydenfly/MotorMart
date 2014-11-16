using System.Collections.Generic;
using MotorMart.Core.Models;
using MotorMart.Core.Models.Validation;
using System;
using MotorMart.Core.Common;
using Elmah;
using MotorMart.Cms.Services;
using MotorMart.Cms.Areas.Misc.Models;


namespace MotorMart.Cms.Areas.Account.Services
{
    public class AccountService : IAccountService
    {
        private IValidationDictionary _validationDictionary;
        private ILinqAccountRepository _accountRepository;

        public AccountService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new LinqAccountRepository())
        { }

        public AccountService(IValidationDictionary validationDictionary, ILinqAccountRepository accountRepository)
        {
            _validationDictionary = validationDictionary;
            _accountRepository = accountRepository;
        }

        #region Helpers
        private string GenerateSecurityKey()
        {
            return Guid.NewGuid().ToString();
        }
        #endregion

        #region Validation

        private bool ValidateUser(useraccount uA)
        {
            return _validationDictionary.IsValid;
        }

        private bool ValidateUserAccount()
        {
            return _validationDictionary.IsValid;
        }

        private bool ValidateUserGroup()
        {
            return _validationDictionary.IsValid;
        }

        #endregion

        #region IAccountService Members

        #region Login

        public bool LoginUser(LoginModel model, IAppCookies iAppCookies)
        {
            if (!_validationDictionary.IsValid) return false;

            try
            {
                useraccount uafromdb = _accountRepository.GetUserAccount(model);

                if (uafromdb != null)
                {
                    //Set the session
                    SessionManager.Current.UserAccountId = uafromdb.useraccountid;

                    //Update user security key
                    uafromdb.securitykey = GenerateSecurityKey();
                    uafromdb.logincount++;
                    uafromdb.datelastloggedin = DateTime.Now;
                    _accountRepository.Update();

                    //Set the cookie
                    iAppCookies.UserAccountId = uafromdb.useraccountid;
                    iAppCookies.UserAccountSecurityKey = uafromdb.securitykey;
                    return true;
                }

                _validationDictionary.AddError("Login", "Incorrect username/password combination");

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return false;
        }

        public void LogoutUser(IAppCookies iAppCookies)
        {
            try
            {
                // Clear the session
                SessionManager.Current.Destroy();

                // Reset the cookie
                iAppCookies.UserAccountId = null;
                iAppCookies.UserAccountSecurityKey = null;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        #endregion

        #region useraccount

        public useraccount GetUserAccount(int UserAccountId, string SecurityKey)
        {
            useraccount UserAccount = new useraccount();
            try
            {
                UserAccount = _accountRepository.GetUserAccount(UserAccountId, SecurityKey);

                if (UserAccount != null)
                {
                    SessionManager.Current.UserAccountId = UserAccount.useraccountid;
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }

            UserAccount = UserAccount == null ? new useraccount() : UserAccount;

            return UserAccount;
        }

        public List<useraccount> GetUserAccountList()
        {
            try
            {
                return _accountRepository.GetUserAccountList();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("_UserAccountLIST", ex.Message);
                return new List<useraccount>();
            }
        }

        public useraccount GetUserAccount(int? Id)
        {
            useraccount UserAccount = new useraccount();
            try
            {
                if (Id.HasValue)
                {
                    UserAccount = _accountRepository.GetUserAccount(Id.Value);
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("_GETUserAccount", ex.Message);
            }

            return UserAccount;
        }

        public bool AddUserAccount(UserAccountAddModel add)
        {
            if (!ValidateUserAccount()) return false;
            try
            {
                useraccount UserAccountToAdd = new useraccount
                {
                    datecreated = DateTime.Now,
                    usergroupid = add.usergroupid,
                    firstname = add.firstname,
                    lastname = add.lastname,
                    email = add.username,
                    telephone = String.Empty,
                    password = add.password,
                    datelastloggedin = DateTime.Now,
                    logincount = 0,
                    securitykey = GenerateSecurityKey(),
                    enabled = add.enabled
                };

                _accountRepository.AddUserAccount(UserAccountToAdd);
                return true;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("_ADDUserAccount", ex.Message);
                return false;
            }
            
        }

        public bool EditUserAccount(UserAccountEditModel edit)
        {
            if (!ValidateUserAccount()) return false;

            try
            {
                useraccount Original = _accountRepository.GetUserAccount(edit.CurrentUserAccount.useraccountid);
                if (Original != null)
                {
                    Original.enabled = edit.CurrentUserAccount.enabled;
                    Original.password = edit.CurrentUserAccount.password;
                    Original.usergroupid = edit.CurrentUserAccount.usergroupid;

                    _accountRepository.Update();
                    return true;
                }
                else
                {
                    throw new Exception("Unable to retrieve for editing");
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("_UserAccountEDIT", ex.Message);
                return false;
            }
            
        }

        #endregion

        #region usergroups

        public List<usergroup> GetUserGroupList()
        {
            try
            {
                return _accountRepository.GetUserGroupList();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("_UserGroupLIST", ex.Message);
                return new List<usergroup>();
            }
        }

        public usergroup GetUserGroup(int? Id)
        {
            usergroup UserGroup = new usergroup();
            try
            {
                if (Id.HasValue)
                {
                    UserGroup = _accountRepository.GetUserGroup(Id.Value);
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("_GETUserGroup", ex.Message);
            }

            return UserGroup;
        }

        #endregion

        #endregion
    }
}
