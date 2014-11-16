using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;

namespace MotorMart.Core.Models
{
    [MetadataType(typeof(dimension))]
    public partial class dimension
    {
        partial void OnValidate(ChangeAction action)
        {
            if (action == ChangeAction.Insert)
            {

            }

            if (action == ChangeAction.Update)
            {
                
            }
        }
    }
}