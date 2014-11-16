using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MotorMart.Core.Models;
using MotorMart.Core.Models.Validation.DataAnnotations;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class DealerGetModel
    {
        public int dealerid { get; set; }
    }

    public class DealerAddModel
    {
        public int dealerid { get; set; }

        public dealer NewDealer { get; set; }

        [Required(ErrorMessage = "A dealer is required!")]
        [DisplayName("Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Owner/dealer address is required")]
        [DisplayName("Owner/ dealer address")]
        public string owneraddress { get; set; }

        [Required(ErrorMessage = "Owner/ dealer postcode is required")]
        [DisplayName("Owner/ dealer postcode")]
        [PostCode(ErrorMessage = "Enter a valid postcode")]
        public string ownerpostcode { get; set; }

        [DisplayName("Country")]
        public int? countryid { get; set; }

        [DisplayName("Coordinates")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string coordinates { get; set; }

        [DisplayName("Website")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string website { get; set; }

        public string filename { get; set; }

        [DisplayName("Logo")]
        public HttpPostedFileBase logo { get; set; }
    }

    public class DealerEditModel
    {
        public int dealerid { get; set; }

        [Required(ErrorMessage = "A dealer is required!")]
        [DisplayName("Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Owner/dealer address is required")]
        [DisplayName("Owner/ dealer address")]
        public string owneraddress { get; set; }

        [Required(ErrorMessage = "Owner/ dealer postcode is required")]
        [DisplayName("Owner/ dealer postcode")]
        [PostCode(ErrorMessage = "Enter a valid postcode")]
        public string ownerpostcode { get; set; }

        [DisplayName("Country")]
        public int? countryid { get; set; }

        [DisplayName("Coordinates")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string coordinates { get; set; }

        [DisplayName("Website")]
        public string website { get; set; }

        public string filename { get; set; }

        [DisplayName("Logo")]
        public HttpPostedFileBase logo { get; set; }
    }

    public class DealerDeleteModel
    {
        public int dealerid { get; set; }

        public dealer CurrentDealer { get; set; }
    }

}