using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;
using MotorMart.Cms.Models;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class BodyTypeViewModel : AdminViewModel
    {
        public IList<bodytype> BodyTypesList;

        public bodytype CurrentBodyType { get; set; }

        public BodyTypeGetModel get { get; set; }
        public BodyTypeAddModel add { get; set; }
        public BodyTypeEditModel edit { get; set; }
        public BodyTypeDeleteModel delete { get; set; }

        public bool Success { get; set; }
    }
}