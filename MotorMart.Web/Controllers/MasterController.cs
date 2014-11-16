using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MotorMart.Web.Services;
using MotorMart.Core.Models.Validation;
using MotorMart.Core.Routing;
using MotorMart.Core.Common;
using MotorMart.Core.Models;
using MotorMart.Core.Services;
using MotorMart.Core.ActionFilterAttributes;
using MotorMart.Web.ActionFilterAttributes;
using MotorMart.Web.Models;
 
namespace MotorMart.Web.Controllers
{
    [Master]
    public abstract class MasterController : Controller
    {
        private IMasterService _service;
        public IHttpContextService httpContextService;

        private MotorMart.Web.Models.MasterViewModel _viewModel;
        public RouteDataBinder RouteDataBinder;
        public readonly IAppCookies _cookies;
        public useraccount _currentUserAccount { get; set; }

        // Set these values via Action Filters on the Controllers
        public object CurrentItem;
        public int CurrentItemId;

        public MasterController(IMasterService service)
        {
            _service = service;
        }

        public MasterController(IAppCookies cookies)
        {
            _cookies = cookies;
        }

        public MasterController(IMasterService service, IHttpContextService httpContextService)
        {
            this._service = service;
            this.httpContextService = httpContextService;
        }

        public MasterController()
        {
            this._service = new MasterService(new ModelStateWrapper(this.ModelState));
            this.httpContextService = new HttpContextService();
            this._cookies = new AppCookies(new CookieContainer());
        }        

        public void PreLoadControllerData()
        {
            //_service.RecreateDb();
            this._currentUserAccount = _service.GetCurrentUserAccount();            
        }
        
        public virtual void LoadGlobalData()
        {
        }

        public virtual void LoadAdditionalGlobalData()
        {
        }
        // Send This data back to our View Model OnActionExecuted[Master]
        public virtual void SetViewModel(MotorMart.Web.Models.MasterViewModel childViewModel)
        {
            // Automatically hook up the current active controller
            _viewModel = childViewModel;
            _viewModel._controller = this;
           
            // Refresh the data sent via routing and send to view
            if (RouteDataBinder != null)
            {
                // Retrieve Global Data
                _viewModel.SitemapList = _service.ListSitemap();                

                RouteDataBinder.Sitemap = _service.GetSitemap(RouteDataBinder.Sitemap.sitemapid);
                _viewModel.CurrentSitemap = RouteDataBinder.Sitemap;

            }

            _viewModel.AppCookies = this._cookies;
            _viewModel.CurrentUserAccount = this._currentUserAccount;

            // Datacontext causing the string replacement in this to update and send back to db. Always to this last!
            PrepareDataForView();
        }        

        private void PrepareDataForView()
        {
            if (RouteDataBinder != null)
            {
                _viewModel.StaticContent = _service.GetStaticContent(RouteDataBinder);
            }
            else
            {
                //_viewModel.StaticContent = _service.GetStaticContent(CurrentItemId, CurrentContentType, RouteDataBinder.PageTemplateItem);
            }
            
        }
    }
}