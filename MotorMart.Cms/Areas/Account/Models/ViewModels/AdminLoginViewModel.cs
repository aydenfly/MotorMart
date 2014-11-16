using MotorMart.Core.Models;
using MotorMart.Cms.Areas.Account.Models;

namespace MotorMart.Cms.Areas.Account.Models
{
    public class AdminLoginViewModel : MasterViewModel
    {
        public useraccount UserAccount { get; set; }

        public AdminLoginViewModel()
        {
            if (UserAccount == null) UserAccount = new useraccount();
        }

        public LoginModel login { get; set; }
    }
}
