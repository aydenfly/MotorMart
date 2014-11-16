using System;
using System.ComponentModel.DataAnnotations;

namespace MotorMart.Core.Models.Validation.DataAnnotations
{
    public class UrlAttribute : RegularExpressionAttribute
    {
        public UrlAttribute() : base("^http(s)?://([w-]+.)+[w-]+(/[w- ./?%&=]*)?$") { }
    }
}
