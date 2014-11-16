using System;
using MotorMart.Core.Models;
using System.Collections.Generic;

namespace MotorMart.Core.Models
{
    public interface ILinqSitemapRepository
    {
        void AddSitemap(sitemap SitemapToAdd);

        void DeleteSitemap(sitemap SitemapToDelete);

        sitemap GetSitemap(int SitemapId);

        int GetSitemapLevelBasedOnParent(int SitemapParentId);

        IList<sitemap> GetSitemapList();

        void Update();    
    }
}
