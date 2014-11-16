using System;
using MotorMart.Core.Models;
using System.Collections.Generic;

namespace MotorMart.Core.Models
{
    public interface ILinqMasterRepository
    {
        sitemap GetSitemap(int SitemapId);

        staticcontent GetStaticContent(sitemap Sitemap);

        staticcontent GetStaticContent(staticcontent StaticContent);

        IList<sitemap> ListSitemap();

        void AddStaticContent(staticcontent StaticContent);

        staticcontent GetLatestStaticContent();
       
        void Update();
    }
}
