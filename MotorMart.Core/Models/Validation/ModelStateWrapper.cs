using System.Web.Mvc;
using System.Collections;

namespace MotorMart.Core.Models.Validation
{
    public class ModelStateWrapper : IValidationDictionary
    {
        public ModelStateDictionary ModelState { get; set; }

        public ModelStateWrapper(ModelStateDictionary modelState)
        {
            ModelState = modelState;
        }

        public void AddError(string key, string errorMessage)
        {
            ModelState.AddModelError(key, errorMessage);
        }

        public bool IsValid
        {
            get { return ModelState.IsValid; }
        }

        public void AddArrayListErrors(ArrayList list)
        {
            foreach (var item in list)
            {
                ModelState.AddModelError(list.IndexOf(item).ToString(), item.ToString());
            }
        }
    }
}
