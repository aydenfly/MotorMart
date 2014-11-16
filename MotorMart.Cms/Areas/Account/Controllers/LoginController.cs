using System.Web.Mvc;
using MotorMart.Cms.Areas.Account.Models;
using MotorMart.Cms.Areas.Account.Services;
using MotorMart.Core.Models.Validation;
using MotorMart.Cms.Controllers;
using MotorMart.Core.Models;
using MotorMart.Core.Common;


namespace MotorMart.Cms.Areas.Account.Controllers
{
    public class LoginController : AdminMasterController
    {
        private IAccountService _service; 

        public LoginController(IAccountService service)
        {
            _service = service;
        }

        public LoginController()
        {
            _service = new AccountService(new ModelStateWrapper(this.ModelState));
        }
        
        #region Login methods
        public ActionResult Index()
        {
            if (SessionManager.Current.IsAuthenticated())
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new AdminLoginViewModel());
        }

        [HttpPost]
        public ActionResult Index(LoginModel login)
        {
            AdminLoginViewModel viewdata = new AdminLoginViewModel();

            if (_service.LoginUser(login, this._cookies))
            {
                return RedirectToAction("Index", "Home", new { @area = "" });
            }

            viewdata.UserAccount = _currentUserAccount;
            return View(viewdata);
        }

        public ActionResult Logout()
        {
            _service.LogoutUser(this._cookies);
            return RedirectToAction("Index", "Login");
        }
        #endregion
    }
}