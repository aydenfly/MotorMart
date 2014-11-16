using System;
using System.ComponentModel.DataAnnotations;

namespace MotorMart.Core.Models.Validation.DataAnnotations
{
    public class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute() : base("^([0-9a-zA-Z]([-\\.\\w\\+]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$") { }
    }
}
