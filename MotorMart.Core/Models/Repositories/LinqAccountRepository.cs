using System.Collections.Generic;
using System.Linq;
using LinqKit;

namespace MotorMart.Core.Models
{
    public class LinqAccountRepository : ILinqAccountRepository
    {
        MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();  

        public useraccount GetUserAccount(int UserAccountId, string SecurityKey)
        {
            var predicate = PredicateBuilder.True<useraccount>();
            predicate = predicate.And(u=>u.enabled);
            predicate = predicate.And(u=>u.useraccountid == UserAccountId);
            predicate = predicate.And(u=>u.securitykey == SecurityKey);
            return _datacontext.useraccounts.Where(predicate).FirstOrDefault();
        }

        public useraccount GetUserAccount(string username)
        {
            var predicate = PredicateBuilder.True<useraccount>();
            predicate = predicate.And(u=>u.enabled);
            predicate = predicate.And(u => u.email == username);
            return _datacontext.useraccounts.Where(predicate).FirstOrDefault();
        }

        public useraccount GetUserAccount(LoginModel model)
        {
            var predicate = PredicateBuilder.True<useraccount>();
            predicate = predicate.And(u => u.enabled);
            predicate = predicate.And(u => u.email == model.email);
            predicate = predicate.And(u => u.password == model.password);
            return _datacontext.useraccounts.Where(predicate).FirstOrDefault();
        }

        public void Update()
        {
            _datacontext.SubmitChanges();
        }

        public void AddUserAccount(useraccount UserAccountToAdd)
        {
            _datacontext.useraccounts.InsertOnSubmit(UserAccountToAdd);
            _datacontext.SubmitChanges();
        }

        public useraccount GetUserAccount(int p)
        {
            return _datacontext.useraccounts.Where(u => u.useraccountid == p).FirstOrDefault();
        }

        public List<useraccount> GetUserAccountList()
        {
            return _datacontext.useraccounts.ToList();
        }

        public List<usergroup> GetUserGroupList()
        {
            return _datacontext.usergroups.ToList();
        }

        public void AddUserGroup(usergroup UserGroupToAdd)
        {
            _datacontext.usergroups.InsertOnSubmit(UserGroupToAdd);
            _datacontext.SubmitChanges();
        }

        public usergroup GetUserGroup(int p)
        {
            return _datacontext.usergroups.Where(u => u.usergroupid == p).FirstOrDefault();
        }
    }
}
