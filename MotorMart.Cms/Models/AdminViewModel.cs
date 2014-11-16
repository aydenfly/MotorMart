using System.Web.Mvc;
using MotorMart.Core.Models;
using MotorMart.Cms.Controllers;
using MotorMart.Core.Services;
using MotorMart.Core.Common;
using System.Collections.Generic;

namespace MotorMart.Cms.Models
{
    public class AdminViewModel
    {
        public AdminMasterController _controller;
        public IHttpContextService httpContextService;
        public SessionManager SessionManager = SessionManager.Current;
        public IAppCookies AppCookies { get; set; }

        public sitemap CurrentSitemap { get; set; }        
        public bool IsHomePage { get; set; }
        public bool SearchPerformed { get; set; }

        
        public staticcontent StaticContent { get; set; }
        public IList<sitemap> SitemapList { get; set; }
        public IList<vehicle> VehicleList { get; set; }
       
        public useraccount CurrentUserAccount { get; set; }

        public AdminViewModel()
        {
            httpContextService = new HttpContextService();
        }

        public AdminViewModel(IHttpContextService httpContextService)
        {
            this.httpContextService = httpContextService;
        }

        public AdminViewModel(AdminMasterController controller)
        {
            _controller = controller;
        }
    }
}