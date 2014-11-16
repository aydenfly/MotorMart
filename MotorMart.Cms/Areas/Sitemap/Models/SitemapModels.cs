using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MotorMart.Core.Models;
using MotorMart.Core.Models.Validation.DataAnnotations;


namespace MotorMart.Cms.Areas.Sitemap.Models
{
    public class SitemapGetModel
    {
        public int sitemapid { get; set; }
    }

    public class SitemapAddModel
    {
        public int sitemapid { get; set; }

        public int level { get; set; }

        [Required(ErrorMessage = "Section required")]
        [DisplayName("Section")]
        [Integer(ErrorMessage = "Enter a valid sitemap section")]
        public int? sitemapparentid { get; set; }

        [Required(ErrorMessage = "Title required")]
        [DisplayName("Title")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string title { get; set; }

        [Required(ErrorMessage = "Reference required")]
        [DisplayName("URL Reference")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string reference { get; set; }

        [DisplayName("Menu display name")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string menudisplayname { get; set; }

        [DisplayName("Controller")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string controller { get; set; }

        [DisplayName("Action")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string action { get; set; }
        
        [DisplayName("Route Name ")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string routename { get; set; }

        [DisplayName("Route Namespaces")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string routenamespace { get; set; }

        [DisplayName("Override URL")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(250)]
        public string overrideurl { get; set; }

        [DisplayName("Nav CssClass")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string navcssclass { get; set; }

        [DisplayName("Enabled")]
        public bool enabled { get; set; }
        
        [DisplayName("Sort order")]
        [Integer(ErrorMessage = "Enter a valid Sort Order")]
        public int sortorder { get; set; }

        [DisplayName("Visible in menu")]
        public bool menuvisible { get; set; }

        [DisplayName("Visible in footer")]
        public bool footervisible { get; set; }
    }

    public class SitemapEditModel
    {
        public int sitemapid { get; set; }

        public int level { get; set; }

        [Required(ErrorMessage = "Section required")]
        [DisplayName("Section")]
        [Integer(ErrorMessage = "Enter a valid sitemap section")]
        public int? sitemapparentid { get; set; }

        [Required(ErrorMessage = "Title required")]
        [DisplayName("Title")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string title { get; set; }

        [Required(ErrorMessage = "Reference required")]
        [DisplayName("URL Reference")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string reference { get; set; }

        [DisplayName("Menu display name")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string menudisplayname { get; set; }

        [DisplayName("Controller")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string controller { get; set; }

        [DisplayName("Action")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string action { get; set; }

        [DisplayName("Route Name ")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string routename { get; set; }

        [DisplayName("Route Namespaces")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string routenamespace { get; set; }

        [DisplayName("Override URL")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(250)]
        public string overrideurl { get; set; }

        [DisplayName("Nav CssClass")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(50)]
        public string navcssclass { get; set; }

        [DisplayName("Enabled")]
        public bool enabled { get; set; }

        [DisplayName("Sort order")]
        [Integer(ErrorMessage = "Enter a valid Sort Order")]
        public int sortorder { get; set; }

        [DisplayName("Visible in menu")]
        public bool menuvisible { get; set; }

        [DisplayName("Visible in footer")]
        public bool footervisible { get; set; }
    }
    
    public class SitemapDeleteModel
    {
        public sitemap CurrentSitemap;
        public int sitemapid { get; set; }
    }

    public class SitemapStaticContentEditModel
    {
        public int sitemapid { get; set; }

        public int staticcontentid { get; set; }

        [DisplayName("Static content")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string content { get; set; }
    }
}
