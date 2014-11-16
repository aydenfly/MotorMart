using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;

namespace MotorMart.Core.Models
{
    [MetadataType(typeof(dealer))]
    public partial class dealer
    {
        partial void OnValidate(ChangeAction action)
        {
            if (action == ChangeAction.Insert)
            {
               _datemodified = DateTime.Now;
            }

            if (action == ChangeAction.Update)
            {
                _datemodified = DateTime.Now;
            }
        }
    }
}