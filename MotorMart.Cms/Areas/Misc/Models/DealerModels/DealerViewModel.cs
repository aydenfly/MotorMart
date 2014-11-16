using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Cms.Models;
using MotorMart.Core.Models;
using System.Web.Mvc;

namespace MotorMart.Cms.Areas.Misc.Models
{
    public class DealerViewModel : AdminViewModel
    {
        public IList<dealer> DealersList;

        public IList<country> CountryList;

        public SelectList CountrySelect { get; set; }

        public PagedList<dealer> DealerResults;

        public dealer CurrentDealer { get; set; }

        public DealerGetModel get { get; set; }
        public DealerAddModel add { get; set; }
        public DealerEditModel edit { get; set; }
        public DealerDeleteModel delete { get; set; }

        public bool Success { get; set; }

        public int page { get; set; }
    }
}