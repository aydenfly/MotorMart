using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models.Validation;
using MotorMart.Cms.Areas.Sitemap.Models;
using MotorMart.Core.Models;
using MotorMart.Core.Common;
using MotorMart.Core.Common.Enums;
using Elmah;

namespace MotorMart.Cms.Areas.Sitemap.Services
{
    public class SitemapService : ISitemapService
    {
        private IValidationDictionary _validationDictionary;
        private ILinqSitemapRepository _sitemapRepository;

        public SitemapService(IValidationDictionary validationDictionary) : this(validationDictionary, new LinqSitemapRepository()) { }

        public SitemapService(IValidationDictionary validationDictionary, ILinqSitemapRepository sitemapRepository)
        {
            _validationDictionary = validationDictionary;
            _sitemapRepository = sitemapRepository;
        }

        #region Helpers

        private bool GetSitemap(SitemapGetModel model, out sitemap Sitemap)
        {
            bool success = false;
            Sitemap = _sitemapRepository.GetSitemap(model.sitemapid);
            if (Sitemap != null)
            {
                success = true;
            }
            else
            {
                _validationDictionary.AddError("Sitemap", "Invalid sitemap");
                Sitemap = new sitemap();
            }
            return success;
        }

        private SitemapAddModel PopulateSitemapAddModel(SitemapAddModel model)
        {
            if (model == null) model = new SitemapAddModel();

            model.level = 1;

            return model;
        }

        private SitemapEditModel PopulateSitemapEditModel(SitemapGetModel getModel, SitemapEditModel model)
        {
            if (model == null) model = new SitemapEditModel();
            
            model.sitemapid = getModel.sitemapid;
            
            sitemap Sitemap;
            if(GetSitemap(getModel, out Sitemap))
            {
                model.action = Sitemap.action;
                model.controller = Sitemap.controller;
                model.enabled = Sitemap.enabled;
                model.footervisible = Sitemap.footervisible;
                model.level = Sitemap.level;
                model.menudisplayname = Sitemap.menudisplayname;
                model.menuvisible = Sitemap.menuvisible;
                model.navcssclass = Sitemap.navcssclass;
                model.overrideurl = Sitemap.overrideurl;
                model.reference = Sitemap.reference;
                model.routename = Sitemap.routename;
                model.routenamespace = Sitemap.routenamespace;
                model.sitemapparentid = Sitemap.sitemapparentid;
                model.sortorder = Sitemap.sortorder;
                model.title = Sitemap.title;
            }

            return model;
        }

        private SitemapStaticContentEditModel PopulateSitemapStaticContentEditModel(SitemapGetModel getModel, SitemapStaticContentEditModel model)
        {
            if (model == null) model = new SitemapStaticContentEditModel();

            model.sitemapid = getModel.sitemapid;

            sitemap Sitemap = GetSitemap(model.sitemapid);
            if (Sitemap != null)
            {
                model.staticcontentid = Sitemap.staticcontentid;
                model.content = Sitemap.staticcontent.content;
            }

            return model;
        }

        #endregion

        #region ISitemap Services

        public SitemapDialogViewModel PopulateSitemapDialogViewModel(SitemapGetModel getModel, SitemapDialogViewModel model)
        {
            if(model == null) model = new SitemapDialogViewModel();
            try
            {
                sitemap Sitemap;
                if (GetSitemap(new SitemapGetModel { sitemapid = getModel.sitemapid }, out Sitemap))
                {
                    model.delete = new SitemapDeleteModel { CurrentSitemap = Sitemap, sitemapid = Sitemap.sitemapid };
                }
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("Dialog", ex.Message);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return model;
        }

        public SitemapViewModel PopulateSitemapViewModel(SitemapGetModel getModel, SitemapViewModel model)
        {
            if(model == null) model = new SitemapViewModel();
            try
            {
                if (getModel == null) getModel = new SitemapGetModel { sitemapid = 0};
                
                sitemap Sitemap;
                if (GetSitemap(new SitemapGetModel { sitemapid = getModel.sitemapid }, out Sitemap))
                {                    
                    model.delete = new SitemapDeleteModel { CurrentSitemap = Sitemap, sitemapid = Sitemap.sitemapid };
                }

                model.add = PopulateSitemapAddModel(model.add);
                model.edit = PopulateSitemapEditModel(getModel, model.edit);
                model.editstaticcontent = PopulateSitemapStaticContentEditModel(getModel, model.editstaticcontent);
            }
            catch (Exception ex)
            {
                _validationDictionary.AddError("Dialog", ex.Message);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return model;
        }

        public sitemap GetSitemap(int? SitemapId)
        {
            sitemap Sitemap = new sitemap();

            try
            {
                if (SitemapId.HasValue)
                {
                    Sitemap = _sitemapRepository.GetSitemap(SitemapId.Value);
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return new sitemap();
            }

            return Sitemap;
        }

        public IList<sitemap> GetSitemapList()
        {
            try
            {
                return _sitemapRepository.GetSitemapList();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return new List<sitemap>();
            }
        }

        public bool AddSitemap(SitemapAddModel model)
        {
            bool success = false;

            if (!_validationDictionary.IsValid) return false;

            try
            {
                sitemap SitemapToAdd = new sitemap
                {
                    sitemapparentid = model.sitemapparentid,
                    title = model.title,
                    level = _sitemapRepository.GetSitemapLevelBasedOnParent(model.sitemapparentid ?? 0),                    
                    reference = StringHelpers.GenerateUrlReference(model.reference),
                    menudisplayname = model.menudisplayname,
                    controller = model.controller,
                    action = model.action,
                    routename = model.routename,
                    routenamespace = model.routenamespace,
                    staticcontent = new staticcontent { content = String.Empty },
                    overrideurl = model.overrideurl,
                    navcssclass = model.navcssclass,
                    enabled = model.enabled,
                    sortorder = model.sortorder,
                    menuvisible = model.menuvisible,
                    footervisible = model.footervisible
                };

                _sitemapRepository.AddSitemap(SitemapToAdd);
                _sitemapRepository.Update();

                model.sitemapid = SitemapToAdd.sitemapid;

                success = true;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                _validationDictionary.AddError("_add", ex.Message);
            }
            return success;
        }

        public bool EditSitemap(SitemapEditModel model)
        {
            bool success = false;

            if (!_validationDictionary.IsValid) return false;

            try
            {
                sitemap SitemapToEdit = _sitemapRepository.GetSitemap(model.sitemapid);
                if (SitemapToEdit != null)
                {                    
                    SitemapToEdit.title = model.title;                    
                    SitemapToEdit.reference = StringHelpers.GenerateUrlReference(model.reference);
                    SitemapToEdit.menudisplayname = model.menudisplayname;
                    SitemapToEdit.controller = model.controller;
                    SitemapToEdit.action = model.action;
                    SitemapToEdit.routename = model.routename;
                    SitemapToEdit.routenamespace = model.routenamespace;                    
                    SitemapToEdit.overrideurl = model.overrideurl;
                    SitemapToEdit.navcssclass = model.navcssclass;
                    SitemapToEdit.enabled = model.enabled;
                    SitemapToEdit.sortorder = model.sortorder;
                    SitemapToEdit.menuvisible = model.menuvisible;
                    SitemapToEdit.footervisible = model.footervisible;

                    if (model.sitemapparentid == null || model.sitemapparentid == 0)
                    {
                        SitemapToEdit.level = 0;
                    }
                    else
                    {
                        // Ensure sitemap level is set correctly (based on parent's level PLUS one)
                        SitemapToEdit.level = _sitemapRepository.GetSitemapLevelBasedOnParent(model.sitemapparentid ?? 0);
                    }

                    model.sortorder = model.sortorder;
                    _sitemapRepository.Update();
                    success = _validationDictionary.IsValid;
                }
                else
                {
                    _validationDictionary.AddError("_edit", "Unable to retrieve for editing");
                }
            }
            catch (Exception ex)
            {                
                _validationDictionary.AddError("_edit", ex.Message);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return success;
        }

        public bool EditSitemapContent(SitemapStaticContentEditModel model)
        {
            bool success = false;

            if (!_validationDictionary.IsValid) return false;

            try
            {
                sitemap SitemapToEdit;
                if(GetSitemap(new SitemapGetModel { sitemapid = model.sitemapid}, out SitemapToEdit))
                {
                    SitemapToEdit.staticcontent.content = HttpUtility.HtmlDecode(model.content);
                    _sitemapRepository.Update();

                    success = _validationDictionary.IsValid;
                }
                else
                {
                    _validationDictionary.AddError("_edit", "Unable to retrieve for editing");
                }

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            
            return success; 
        }
      
        public bool DeleteSitemap(SitemapDeleteModel model)
        {
            bool success = false;
            if (!_validationDictionary.IsValid) return false;
            try
            {
                sitemap SitemapToDelete;
                if (GetSitemap(new SitemapGetModel { sitemapid = model.sitemapid }, out SitemapToDelete))
                {
                    _sitemapRepository.DeleteSitemap(SitemapToDelete);
                }

                success = _validationDictionary.IsValid;
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleError(ex, _validationDictionary);
            }
            return success;
        }

        #endregion
    }
}