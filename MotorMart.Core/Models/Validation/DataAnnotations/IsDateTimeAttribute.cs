using System;
using System.ComponentModel.DataAnnotations;

namespace MotorMart.Core.Models.Validation.DataAnnotations
{
    public class IsDateTimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                DateTime dt;
                if (DateTime.TryParse(value.ToString(), out dt))
                {
                    if(dt.Year > 1753 && dt.Year < 9999) return true;
                }
            }
            return false;
        }
    }
}
