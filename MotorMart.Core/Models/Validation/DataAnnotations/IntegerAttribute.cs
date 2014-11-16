using System;
using System.ComponentModel.DataAnnotations;

namespace MotorMart.Core.Models.Validation.DataAnnotations
{
    public class IntegerAttribute : RegularExpressionAttribute
    {
        public IntegerAttribute() : base("^([0-9]*)$") { }
    }
}
