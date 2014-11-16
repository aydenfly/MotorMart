using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace MotorMart.Core.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        [DisplayName("Username / Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DisplayName("Password")]
        public string password { get; set; }
    }
}