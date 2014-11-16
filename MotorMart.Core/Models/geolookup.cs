using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;

namespace MotorMart.Core.Models
{
    [MetadataType(typeof(geolookup))]
    public partial class geolookup
    {
        partial void OnValidate(ChangeAction action)
        {
            if (action == ChangeAction.Insert)
            {
                _datefetched = DateTime.Now;
            }

            if (action == ChangeAction.Update)
            {
                _datefetched = DateTime.Now;
            }
        }
    }
}