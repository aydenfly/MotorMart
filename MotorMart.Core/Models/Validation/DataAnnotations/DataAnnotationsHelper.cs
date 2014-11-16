using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MotorMart.Core.Models.Validation.DataAnnotations
{
    public class DataAnnotationsHelper
    {
        public static void RegisterAllAdapters()
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredWhenPropertyEqualToAttribute), typeof(RequiredWhenPropertyEqualToValidator));
        }
    }
}
