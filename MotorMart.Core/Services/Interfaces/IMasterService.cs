using System.Collections.Generic;
using MotorMart.Core.Routing;
using MotorMart.Core.Models;

namespace MotorMart.Core.Services
{
    public interface IMasterService
    {
        void AddStaticContent(staticcontent StaticContent);

        sitemap GetSitemap(int SitemapId);

        staticcontent GetStaticContent(RouteDataBinder RouteDataBinder);

        IList<sitemap> ListSitemap();

        useraccount GetCurrentUserAccount();
    }
}
