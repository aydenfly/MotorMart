using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Cms.Models;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class UserAccountViewModel : MasterViewModel
    {
        public UserAccountAddModel add { get; set; }
        public UserAccountEditModel edit { get; set; }
        public UserAccountDeleteModel delete { get; set; }
    }
}