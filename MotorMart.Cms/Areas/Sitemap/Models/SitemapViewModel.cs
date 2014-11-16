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
using MotorMart.Core.Models;

namespace MotorMart.Cms.Areas.Sitemap.Models
{
    public class SitemapViewModel : AdminViewModel
    {
        public SitemapDeleteModel delete { get; set; }

        public SitemapGetModel get { get; set; }

        public SitemapAddModel add { get; set; }

        public SitemapEditModel edit { get; set; }

        public SitemapStaticContentEditModel editstaticcontent { get; set; }

        public bool Success { get; set; }
    }
}
