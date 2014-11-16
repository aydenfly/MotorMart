using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Core.Controllers;
using MotorMart.Core.Common;
using MotorMart.Core.Models;
using MotorMart.Core.Services;

namespace MotorMart.Core.Models
{
    public class MasterViewModel
    {
        public MasterController _controller;
        public IHttpContextService httpContextService;
        public SessionManager SessionManager = SessionManager.Current;
        public IAppCookies AppCookies { get; set; }

        public IList<sitemap> SitemapList { get; set; }
        
        public sitemap CurrentSitemap { get; set; }        
        public bool IsHomePage { get; set; }
        public bool SearchPerformed { get; set; }

        
        public staticcontent StaticContent { get; set; }
        public IList<vehicle> VehicleList { get; set; }
       
        public useraccount CurrentUserAccount { get; set; }

        public MasterViewModel()
        {
            httpContextService = new HttpContextService();
        }

        public MasterViewModel(IHttpContextService httpContextService)
        {
            this.httpContextService = httpContextService;
        }

        public MasterViewModel(MasterController controller)
        {
            _controller = controller;
        }
    }
}