using System;
using System.ComponentModel.DataAnnotations;

namespace MotorMart.Core.Models.Validation.DataAnnotations
{
    public class PostCodeAttribute : RegularExpressionAttribute
    {
        //public PostCodeAttribute() : base("^(GIR 0AA|[A-PR-UWYZ]([0-9]{1,2}|([A-HK-Y][0-9]|[A-HK-Y][0-9]([0-9]|[ABEHMNPRV-Y]))|[0-9][A-HJKS-UW]) [0-9][ABD-HJLNP-UW-Z]{2})$") { }
        public PostCodeAttribute() : base("^[A-Za-z]{1,2}[0-9R][0-9A-Z]?[ ]?[0-9][A-Za-z-[CIKMOVcikmov]]{2}$") { }
    }
}
