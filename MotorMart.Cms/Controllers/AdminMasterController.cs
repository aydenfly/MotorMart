using System.Collections.Generic;
using System.Web.Mvc;
using MotorMart.Core.Common;
using MotorMart.Cms.Services;
using MotorMart.Core.Models.Validation;
using MotorMart.Cms.Models;
using MotorMart.Core.Services;
using MotorMart.Core.Models;
using MotorMart.Cms.ActionFilterAttributes;
using MotorMart.Core.ActionFilterAttributes;


namespace MotorMart.Cms.Controllers
{
    [AdminAuthentication(Order = 0)]
    [RebuildRoutes(Order = 1)]
    public abstract class AdminMasterController : Controller
    {
        private IMasterService _service;
        public IHttpContextService httpContextService;
        public readonly IAppCookies _cookies;
        public useraccount _currentUserAccount { get; set; }
        public AdminViewModel _adminViewModel;

        public AdminMasterController(IMasterService service)
        {
            _service = service;
        }

        public AdminMasterController(IAppCookies cookies)
        {
            _cookies = cookies;
        }

        public AdminMasterController(IMasterService service, IHttpContextService httpContextService)
        {
            this._service = service;
            this.httpContextService = httpContextService;
        }

        public AdminMasterController()
        {
            this._service = new MasterService(new ModelStateWrapper(this.ModelState));
            this.httpContextService = new HttpContextService();
            this._cookies = new AppCookies(new CookieContainer());
        }        

        public void PreLoadControllerData()
        {
            this._currentUserAccount = _service.GetCurrentUserAccount();            
        }

        public ActionResult RedirectToErrorPage()
        {
            return RedirectToAction("index", "error", new { });
        }

        public virtual void SetAdminViewModel(AdminViewModel viewModel)
        {
            _adminViewModel = viewModel;
            _adminViewModel._controller = this;
            _adminViewModel.CurrentUserAccount = this._currentUserAccount;
            //_currentUserAccount = viewModel.CurrentUserAccount;
        }
    }
}
