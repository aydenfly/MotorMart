using System;
using MotorMart.Core.Models;

namespace MotorMart.Core.Models
{
    public interface ILinqUserRepository
    {
        void AddUserAccount(useraccount UserAccountToAdd);

        useraccount GetUserAccount(int userAccountId);

        useraccount GetUserAccount(string username, string password);
    }
}
