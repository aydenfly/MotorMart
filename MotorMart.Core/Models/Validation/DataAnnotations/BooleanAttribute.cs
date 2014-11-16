using System;
using System.ComponentModel.DataAnnotations;

namespace MotorMart.Core.Models.Validation.DataAnnotations
{
    public class BooleanAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || Convert.ToBoolean(value) == false)
            {
                return false;
            }
            return true;
        }
    }
}
