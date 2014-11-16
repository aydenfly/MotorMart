using System;
using System.Collections.Generic;
using System.Linq;
using MotorMart.Core.Models;
using MotorMart.Core.Models.Validation;
using MotorMart.Core.Common;
using Elmah;

namespace MotorMart.Cms.Services
{
    public class AdminService : IAdminService
    {
        private IValidationDictionary _validationDictionary;
        private ILinqVehicleRepository _vehicleRepository;
        private ILinqSitemapRepository _sitemapRepository;

        public AdminService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new LinqVehicleRepository(), new LinqSitemapRepository())
        { }

        public AdminService(IValidationDictionary validationDictionary, ILinqVehicleRepository vehicleRepository, ILinqSitemapRepository sitemapRepository)
        {
            _validationDictionary = validationDictionary;
            _vehicleRepository = vehicleRepository;
            _sitemapRepository = sitemapRepository;
        }

        #region IAdminService Members

        public IList<sitemap> GetSitemapList()
        {
            try
            {
                return _sitemapRepository.GetSitemapList();
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleError(ex, _validationDictionary);
            }

            return new List<sitemap>();
        }

        public IList<vehicle> GetVehicleList()
        {
            try
            {
                return _vehicleRepository.ListVehicles();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new List<vehicle>();
        }

        public IQueryable<vehicle> SearchVehicles(VehicleSearchModel model)
        {
            try
            {
                return _vehicleRepository.SearchVehicles(model);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return new List<vehicle>().AsQueryable();
        }

        #endregion
    }
}
