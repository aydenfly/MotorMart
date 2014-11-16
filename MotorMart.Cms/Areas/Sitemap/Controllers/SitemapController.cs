using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorMart.Cms.Controllers;
using MotorMart.Cms.Areas.Sitemap.Services;
using MotorMart.Core.Models.Validation;
using MotorMart.Cms.Areas.Sitemap.Models;

namespace MotorMart.Cms.Areas.Sitemap.Controllers
{
    public class SitemapController : AdminMasterController
    {
        private ISitemapService _service;

        public SitemapController(ISitemapService service)
        {
            _service = service;
        }

        public SitemapController()
        {
            _service = new SitemapService(new ModelStateWrapper(this.ModelState));
        }

        private void LoadViewData(SitemapViewModel viewdata)
        {
            viewdata.SitemapList = _service.GetSitemapList();
            viewdata = _service.PopulateSitemapViewModel(viewdata.get, viewdata);
        }

        public ActionResult Index()
        {
            SitemapViewModel viewdata = new SitemapViewModel();
            LoadViewData(viewdata);
            return View(viewdata);
        }

        public ActionResult Add()
        {
            SitemapViewModel viewdata = new SitemapViewModel();
            LoadViewData(viewdata);
            return View(viewdata);
        }

        [HttpPost]
        public ActionResult Add(SitemapAddModel add)
        {            
            if (_service.AddSitemap(add))
            {
                return RedirectToAction("edit", new { @sitemapid = add.sitemapid });
            }

            SitemapViewModel viewdata = new SitemapViewModel();
            LoadViewData(viewdata);
            return View(viewdata);
        }

        public ActionResult Edit(SitemapGetModel model)
        {
            SitemapViewModel viewdata = new SitemapViewModel();
            viewdata.CurrentSitemap = _service.GetSitemap(model.sitemapid);
            if (viewdata.CurrentSitemap != null)
            {
                //SiteMap.CurrentNode.Title = viewdata.CurrentSitemap.title;
                viewdata.get = model;
                LoadViewData(viewdata);
                return View(viewdata);
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Edit(SitemapEditModel edit)
        {
            SitemapViewModel viewdata = new SitemapViewModel();
            if (_service.EditSitemap(edit))
            {
                return RedirectToAction("edit", new { @sitemapid = edit.sitemapid });
            }
            viewdata.get = new SitemapGetModel { sitemapid = edit.sitemapid };
            LoadViewData(viewdata);

            return View(viewdata);
        }

        public ActionResult EditStaticContent(SitemapGetModel model)
        {
            SitemapViewModel viewdata = new SitemapViewModel();
            viewdata.CurrentSitemap = _service.GetSitemap(model.sitemapid);
            if (viewdata.CurrentSitemap != null)
            {
                //SiteMap.CurrentNode.Title = viewdata.CurrentSitemap.title;
                viewdata.get = model;
                LoadViewData(viewdata);
                return View(viewdata);
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult EditStaticContent(SitemapStaticContentEditModel editstaticcontent)
        {
            SitemapViewModel viewdata = new SitemapViewModel();
            if (_service.EditSitemapContent(editstaticcontent))
            {
                return RedirectToAction("editstaticcontent", new { @sitemapid = editstaticcontent.sitemapid });
            }
            viewdata.get = new SitemapGetModel { sitemapid = editstaticcontent.sitemapid };
            LoadViewData(viewdata);

            return View(viewdata);
        }

        public ActionResult SitemapDeleteDialog(SitemapGetModel model)
        {
            SitemapDialogViewModel dialog = _service.PopulateSitemapDialogViewModel(model, null);
            return PartialView("SitemapDeleteDialog", dialog);
        }

        [HttpPost]
        public ActionResult Delete(SitemapDeleteModel delete)
        {
            SitemapViewModel model = new SitemapViewModel { Success = false };
            if (_service.DeleteSitemap(delete))
            {
                model.Success = true;
            }
            model.get = new SitemapGetModel { sitemapid = delete.sitemapid };
            LoadViewData(model);

            return View(model);
        }
    }
}
