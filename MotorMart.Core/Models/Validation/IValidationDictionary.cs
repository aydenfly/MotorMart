using System;
using System.Web.Mvc;
using System.Collections;

namespace MotorMart.Core.Models.Validation
{
    public interface IValidationDictionary
    {
        void AddError(string key, string errorMessage);
        void AddArrayListErrors(ArrayList list);
        bool IsValid { get; }
        ModelStateDictionary ModelState { get; set; }
    }
}
