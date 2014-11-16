using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MotorMart.Core.Models;
using MotorMart.Core.Models.Validation.DataAnnotations;


namespace MotorMart.Cms.Areas.Misc.Models
{
    public class UserAccountEditModel
    {
        public useraccount CurrentUserAccount { get; set; }
        
        public SelectList UserGroupSelectList { get; set; }
        public int usergroupid { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [DisplayName("First name")]
        [StringLength(50)]
        public string firstname { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [DisplayName("Last name")]
        [StringLength(50)]
        public string lastname { get; set; }

        [Required(ErrorMessage = "Username / Email is required")]
        [DisplayName("Username / Email")]
        [StringLength(150)]
        [Email(ErrorMessage = "Please enter a valid email")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DisplayName("Password")]
        [StringLength(50)]
        public string password { get; set; }

        [DisplayName("Enabled")]
        public bool enabled { get; set; }

    }
}