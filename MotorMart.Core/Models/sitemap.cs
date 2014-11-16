using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Linq.Expressions;

namespace MotorMart.Core.Models
{
    public partial class sitemap
    {
        partial void OnCreated()
        {
            this._level = 0;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (action == ChangeAction.Insert || action == ChangeAction.Update)
            {
                if (String.IsNullOrEmpty(this._routename)) this._routename = DateTime.Now.ToString();
                if (String.IsNullOrEmpty(this._controller)) this._controller = MotorMart.Core.Common.Enums.Controllers.Cms.ToString();
                if (String.IsNullOrEmpty(this._action)) this._action = String.Empty;
                if (String.IsNullOrEmpty(this._overrideurl)) this._overrideurl = String.Empty;
                if (String.IsNullOrEmpty(this._menudisplayname)) this._menudisplayname = String.Empty;
                if (String.IsNullOrEmpty(this._navcssclass)) this._navcssclass = String.Empty;
                if (String.IsNullOrEmpty(this._routenamespaces)) this._routenamespaces = String.Empty;
            }

            if (action == ChangeAction.Update)
            {
            }
        }

        public static Expression<Func<sitemap, bool>> IsEnabled()
        {
            return p => p.enabled;
        }
    }
}
