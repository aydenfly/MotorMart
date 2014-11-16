using System.Web.Mvc;
using MotorMart.Cms.Models;
using MotorMart.Cms.ActionFilterAttributes;
using MotorMart.Cms.Services;
using MotorMart.Core.Models.Validation;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Controllers
{
    public class HomeController : AdminMasterController
    {
        public ActionResult Index()
        {
            AdminViewModel model = new AdminViewModel();
            return View(model);
        }
    }
}