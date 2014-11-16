using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MotorMart.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class BodyTypeGetModel
    {
        public int bodytypeid { get; set; }
    }

    public class BodyTypeAddModel
    {
        public int bodytypeid { get; set; }

        public bodytype NewBodyType { get; set; }

        [Required(ErrorMessage = "Body type is required!")]
        [DisplayName("Type")]
        public string type { get; set; }

        [DisplayName("Sort order")]
        public int sortorder { get; set; }
    }

    public class BodyTypeEditModel
    {
        public int bodytypeid { get; set; }

        [Required(ErrorMessage = "Body type is required!")]
        [DisplayName("Type")]
        public string type { get; set; }

        [DisplayName("Sort order")]
        public int sortorder { get; set; }
    }

    public class BodyTypeDeleteModel
    {
        public int bodytypeid { get; set; }
        public bodytype CurrentBodyType { get; set; }
    }
}