using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;

namespace MotorMart.Core.Models
{
    [MetadataType(typeof(feature))]
    public partial class feature
    {
        partial void OnValidate(ChangeAction action)
        {
            if (action == ChangeAction.Insert)
            {
                if (_interiordetails == null) _interiordetails = String.Empty;
                if (_exteriordetails == null) _exteriordetails = String.Empty;
            }

            if (action == ChangeAction.Update)
            {

            }
        }
    }
}