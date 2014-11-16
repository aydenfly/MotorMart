using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MotorMart.Core.Enums;
using MotorMart.Core.Models;
using LinqKit;
using Elmah;


namespace MotorMart.Core.Models
{
    public class LinqSitemapRepository : ILinqSitemapRepository
    {
        MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();

        public sitemap GetSitemap(int SitemapId)
        {
            return _datacontext.sitemaps.Where(s => s.sitemapid == SitemapId).FirstOrDefault();
        }

        public void AddSitemap(sitemap SitemapToAdd)
        {
            _datacontext.sitemaps.InsertOnSubmit(SitemapToAdd);
            Update();
        }

        public void DeleteSitemap(sitemap SitemapToDelete)
        {
            _datacontext.sitemaps.DeleteOnSubmit(SitemapToDelete);
            Update();
        }

        public IList<sitemap> GetSitemapList()
        {
            return _datacontext.sitemaps.ToList();
        }

        public int GetSitemapLevelBasedOnParent(int SitemapParentId)
        {
            int result = 1;
            try
            {
                sitemap parentSitemap = _datacontext.sitemaps.Where(s => s.sitemapid == SitemapParentId).FirstOrDefault();
                result = parentSitemap.level + 1;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return result;
        }

        public void Update()
        {
            _datacontext.SubmitChanges();
        }

    }
}
