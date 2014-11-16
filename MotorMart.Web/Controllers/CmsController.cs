using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MotorMart.Web.Models;
using MotorMart.Web.Services;
using MotorMart.Core.Models.Validation;
using MotorMart.Core.Models;
using System.ServiceModel.Syndication;
using MotorMart.Core.ActionResults;
using System.Threading;

namespace MotorMart.Web.Controllers
{
    public class CmsController : MasterController
    {

        private IVehicleService _vehicleService;
        private FeedService _feedService;

        public CmsController()
        {
            _vehicleService = new VehicleService(new ModelStateWrapper(this.ModelState));
            _feedService = new FeedService();
        }

        public ActionResult Index()
        {
            CmsViewModel model = new CmsViewModel();
            return View(model);
        }

        public ActionResult JCrop()
        {
            CmsViewModel model = new CmsViewModel
                {
                    AvailableVehicles = _vehicleService.ListVehicles().ToList(),
                    RequestedVehicles = new List<vehicle>()
                };

            return View(model);
        }

        [HttpPost]
        public ActionResult JCrop(CmsViewModel model, string add, string remove)
        {
            //Need to clear model state or it will interfere with the updated model
            ModelState.Clear();
            RestoreSavedState(model);
            if (!string.IsNullOrEmpty(add))
            {
                AddVehicles(model);
            }
            else if (!string.IsNullOrEmpty(remove))
            {
                RemoveVehicles(model);
            }
            SaveState(model);
            return View(model);

        }

        public ActionResult Video()
        {
            CmsViewModel model = new CmsViewModel();

            model.videoFeed = _feedService.GetYouTubeFeed();

            return View(model);
        }

        public ActionResult NewsFeed()
        {
            CmsViewModel model = new CmsViewModel();

            //model.newsFeed = _feedService.GetNewsFeed();
            model.newsFeedItems = _feedService.GetNewsFeedItems();

            return View(model);
        }

        public ActionResult GeneratePdf(GeneratePdfModel generatepdf)
        {
            string pdfVirtualPath;
            string pdfFileName = _feedService.GeneratePdf(generatepdf, out pdfVirtualPath);
            if (!String.IsNullOrEmpty(pdfFileName))
            {
                //pdfVirtualPath = Server.MapPath("/Resources/files/pdfs/" + pdfFileName);
                //pdfVirtualPath =  Server.MapPath(Path.Combine("/Resources/files/pdfs/", pdfFileName));

                return new DownloadActionResult
                    {
                        FileDownloadName = pdfFileName,
                        VirtualPath = pdfVirtualPath,
                        ContentType = "application/pdf"
                    };
            }

            return View(new CmsViewModel());
        }

        public ActionResult FancyChart()
        {
            var model = new CmsViewModel();
            return View(model);
        }

        public ActionResult CropImage()
        {
            ProfileViewModel pmodel = new ProfileViewModel();

            return View(pmodel);
        }

        //public ActionResult UploadImage(ProfileViewModel model)
        //{
        //    var image = WebImage.GetImageFromRequest();

        //    if (image != null)
        //    {
        //        if (image.Width > 500)
        //        {
        //            image.Resize(500, ((500*image.Height)/image.Width));
        //        }

        //        var filename = Path.GetFileName(image.FileName);
        //        image.Save(Path.Combine("~/Images", filename));
        //        filename = Path.Combine("~/Images", filename);
        //        model.ImageUrl = Url.Content(filename);
        //        var editModel = new EditorInputModel()
        //            {
        //                Profile = model,
        //                Width = image.Width,
        //                Height = image.Height,
        //                Top = image.Height*0.1,
        //                Left = image.Width*0.9,
        //                Right = image.Width*0.9,
        //                Bottom = image.Height*0.9
        //            };
        //        return View("ImageCrop", editModel);

        //    }

        //    return View("ImageCrop", model);
        //}

        //public ActionResult Edit(EditorInputModel editor)
        //{
        //    var image = new WebImage("~" + editor.Profile.ImageUrl);
        //    var height = image.Height;
        //    var width = image.Width;
        //    image.Crop((int) editor.Top, (int) editor.Left, (int) (height - editor.Bottom), (int) (width - editor.Right));
        //    var originalFile = editor.Profile.ImageUrl;
        //    editor.Profile.ImageUrl = Url.Content("~/ProfileImages/" + Path.GetFileName(image.FileName));
        //    image.Resize(100, 100, true, false);
        //    image.Save(@"~" + editor.Profile.ImageUrl);
        //    System.IO.File.Delete(Server.MapPath(originalFile));
        //    return View("Index", editor.Profile);
        //}

        #region SupportFuncs

        private void Validate(CmsViewModel model)
        {
            if (string.IsNullOrEmpty(model.SavedRequested))
                ModelState.AddModelError("", "You haven't selected any presents!");
        }

        private void SaveState(CmsViewModel model)
        {
            //create comma delimited list of product ids
            model.SavedRequested = string.Join(",",
                                               model.RequestedVehicles.Select(p => p.vehicleid.ToString()).ToArray());

            //Available vehicles = All - Requested
            model.AvailableVehicles = _vehicleService.ListVehicles().Except(model.RequestedVehicles).ToList();
        }

        private void RemoveVehicles(CmsViewModel model)
        {
            if (model.RequestedSelected != null)
            {
                model.RequestedVehicles.RemoveAll(p => model.RequestedSelected.Contains(p.vehicleid));
                model.RequestedSelected = null;
            }
        }

        private void AddVehicles(CmsViewModel model)
        {
            if (model.AvailableSelected != null)
            {
                var _vehicles = _vehicleService.ListVehicles().Where(p => model.AvailableSelected.Contains(p.vehicleid));
                model.RequestedVehicles.AddRange(_vehicles);
                model.AvailableSelected = null;
            }
        }

        private void RestoreSavedState(CmsViewModel model)
        {
            model.RequestedVehicles = new List<vehicle>();

            //get the previously stored items
            if (!string.IsNullOrEmpty(model.SavedRequested))
            {
                string[] _vehicleIds = model.SavedRequested.Split(',');
                var prods = _vehicleService.ListVehicles().Where(p => _vehicleIds.Contains(p.vehicleid.ToString()));
                model.RequestedVehicles.AddRange(prods);
            }
        }

        #endregion

    }
}
