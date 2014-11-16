using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;

namespace MotorMart.Core.Models
{
    [MetadataType(typeof(safetydetail))]
    public partial class safetydetail
    {
        partial void OnValidate(ChangeAction action)
        {
            if (action == ChangeAction.Insert)
            {
                if (_details == null) _details = String.Empty;
            }

            if (action == ChangeAction.Update)
            {

            }
        }
    }
}