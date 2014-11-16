using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using MotorMart.Core.Routing;
using MotorMart.Core.Models;
using MotorMart.Core.Models.Validation;
using MotorMart.Core.Common;

namespace MotorMart.Core.Services
{
    public class MasterService : IMasterService
    {
        private IValidationDictionary _validationDictionary;
        private ILinqMasterRepository _repository;
        private ILinqUserRepository _userRepository;
        private IList<sitemap> _entireSitemap;

        public MasterService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new LinqMasterRepository(), new LinqUserRepository())
        { }

        public MasterService(IValidationDictionary validationDictionary, ILinqMasterRepository repository, ILinqUserRepository userRepository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
            _userRepository = userRepository;
        }

        private string GenerateCookieId()
        {
            return Guid.NewGuid().ToString() + DateTime.Now.Ticks;
        }

        #region IMasterService Members

        public IList<sitemap> ListSitemap()
        {
            try
            {
                _entireSitemap = _repository.ListSitemap();
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleError(ex);
            }

            if (_entireSitemap == null) _entireSitemap = new List<sitemap>();

            return _entireSitemap;
        }

        public sitemap GetSitemap(int SitemapId)
        {
            try
            {
                return _repository.GetSitemap(SitemapId);
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleError(ex);
                return new sitemap();
            }
        }
        
        public staticcontent GetStaticContent(RouteDataBinder RouteDataBinder)
        {
            staticcontent StaticContent = new staticcontent();

            if (RouteDataBinder != null)
            {
                try
                {
                    StaticContent = _repository.GetStaticContent(RouteDataBinder.Sitemap);
                }
                catch (Exception ex)
                {
                    ErrorHelper.HandleError(ex);
                    string error = ex.Message;
                }
            }

            return StaticContent;
        }
        
        public void AddStaticContent(staticcontent StaticContent)
        {
            if (StaticContent != null)
            {
                try
                {
                    _repository.AddStaticContent(StaticContent);
                }
                catch (Exception ex)
                {
                    ErrorHelper.HandleError(ex);
                }
            }
        }

        public useraccount GetCurrentUserAccount()
        {
            if (SessionManager.Current.UserAccountId > 0)
            {
                return _userRepository.GetUserAccount(SessionManager.Current.UserAccountId);
            }
            else
            {
                return new useraccount();
            }
        }

        #endregion
    }
}
