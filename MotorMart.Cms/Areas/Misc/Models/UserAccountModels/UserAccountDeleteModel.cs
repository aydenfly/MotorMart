using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MotorMart.Core.Models;
using MotorMart.Core.Models.Validation.DataAnnotations;


namespace MotorMart.Cms.Areas.Misc.Models
{
    public class UserAccountDeleteModel
    {
        public int useraccountid { get; set; }

    }
}