using System;
using System.Linq;
using System.Collections.Generic;
using MotorMart.Core.Models;

namespace MotorMart.Cms.Services
{
    public interface IAdminService
    {
        IList<sitemap> GetSitemapList();

        IList<vehicle> GetVehicleList();
        
        IQueryable<vehicle> SearchVehicles(VehicleSearchModel model);
    }
}
