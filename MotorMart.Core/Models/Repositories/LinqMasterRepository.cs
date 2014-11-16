using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MotorMart.Core.Models
{
    public class LinqMasterRepository : ILinqMasterRepository
    {
        private MotorMartDBDataContext _datacontext = new MotorMartDBDataContext();

        public IList<sitemap> ListSitemap()
        {            
            return _datacontext.sitemaps.ToList();
        }

        public sitemap GetSitemap(int SitemapId)
        {
            return _datacontext.sitemaps.Where(s => s.sitemapid == SitemapId).FirstOrDefault();
        }

        public staticcontent GetStaticContent(staticcontent StaticContent)
        {
            return _datacontext.staticcontents.Where(s => s.staticcontentid == StaticContent.staticcontentid).FirstOrDefault();
        }

        public staticcontent GetStaticContent(sitemap Sitemap)
        {
            return _datacontext.sitemaps.Where(s => s.sitemapid == Sitemap.sitemapid).Select(s => s.staticcontent).FirstOrDefault();
        }

        public void AddStaticContent(staticcontent StaticContent)
        {
            _datacontext.staticcontents.InsertOnSubmit(StaticContent);
            _datacontext.SubmitChanges();
        }

        public staticcontent GetLatestStaticContent()
        {
            return _datacontext.staticcontents.OrderByDescending(s => s.staticcontentid).FirstOrDefault();
        }

        public void Update()
        {            
            _datacontext.SubmitChanges();
        }
    }
}
