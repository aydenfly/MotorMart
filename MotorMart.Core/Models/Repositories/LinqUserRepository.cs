using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqKit;


namespace MotorMart.Core.Models
{
    public class LinqUserRepository : ILinqUserRepository
    {
        MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();        

        public useraccount GetUserAccount(int userAccountId)
        {
            return _datacontext.useraccounts.Where(u => u.useraccountid == userAccountId).FirstOrDefault();
        }

        public useraccount GetUserAccount(string username, string password)
        {
            return _datacontext.useraccounts.Where(u => u.email == username && u.password == password).FirstOrDefault();
        }

        public void AddUserAccount(useraccount UserAccountToAdd)
        {           
            _datacontext.useraccounts.InsertOnSubmit(UserAccountToAdd);
            _datacontext.SubmitChanges();
        }
    }
}
