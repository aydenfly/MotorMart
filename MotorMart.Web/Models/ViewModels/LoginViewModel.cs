using MotorMart.Core.Models;

namespace MotorMart.Web.Models
{
    public class LoginViewModel : MasterViewModel
    {
        public useraccount UserAccount { get; set; }

        public LoginViewModel()
        {
            if (UserAccount == null) UserAccount = new useraccount();
        }

        public LoginModel login { get; set; }
    }
}
