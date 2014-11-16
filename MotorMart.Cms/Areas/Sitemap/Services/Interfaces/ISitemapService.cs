using System;
using MotorMart.Cms.Areas.Sitemap.Models;
using System.Collections.Generic;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Areas.Sitemap.Services
{
    public interface ISitemapService
    {
        bool AddSitemap(SitemapAddModel model);

        bool DeleteSitemap(SitemapDeleteModel model);

        bool EditSitemap(SitemapEditModel model);

        bool EditSitemapContent(SitemapStaticContentEditModel model);

        sitemap GetSitemap(int? SitemapId);

        IList<sitemap> GetSitemapList();

        SitemapDialogViewModel PopulateSitemapDialogViewModel(SitemapGetModel getModel, SitemapDialogViewModel model);

        SitemapViewModel PopulateSitemapViewModel(SitemapGetModel getModel, SitemapViewModel model);
    }
}
