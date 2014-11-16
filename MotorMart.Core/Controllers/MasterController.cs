using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MotorMart.Core.Models.Validation;
using MotorMart.Core.Routing;
using MotorMart.Core.Common;
using MotorMart.Core.Models;
using MotorMart.Core.Services;
using MotorMart.Core.ActionFilterAttributes;
 
namespace MotorMart.Core.Controllers
{
    [UpdateCookies]    
    [UpdateRouteRegistration(Order = 0)]
    [HandleErrorWithElmah]
    public abstract class MasterController : Controller
    {
        private IMasterService _service;
        public IHttpContextService httpContextService;
        //public IVehicleService _vehicleService;

        private MasterViewModel _viewModel;
        public RouteDataBinder RouteDataBinder;
        public readonly IAppCookies _cookies;
        public useraccount CurrentUserAccount { get; set; }

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
            //this._vehicleService = new VehicleService(new ModelStateWrapper(this.ModelState));
            this.httpContextService = new HttpContextService();
            this._cookies = new AppCookies(new CookieContainer());
        }        

        public void PreLoadControllerData()
        {
            //_service.RecreateDb();
            this.CurrentUserAccount = _service.GetCurrentUserAccount();            
        }
        
        public virtual void LoadGlobalData()
        {
        }

        public virtual void LoadAdditionalGlobalData()
        {
        }
        // Send This data back to our View Model OnActionExecuted[Master]
        public virtual void SetViewModel(MasterViewModel childViewModel)
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
            _viewModel.CurrentUserAccount = this.CurrentUserAccount;

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