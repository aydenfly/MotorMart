using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MotorMart.Core.Models.Validation.DataAnnotations;
using MotorMart.Cms.Areas.Sitemap.Models;
using MotorMart.Cms.Models;

namespace MotorMart.Cms.Areas.Sitemap.Models
{
    public class SitemapDialogViewModel : AdminViewModel
    {
        public SitemapDeleteModel delete { get; set; }
    }
}
