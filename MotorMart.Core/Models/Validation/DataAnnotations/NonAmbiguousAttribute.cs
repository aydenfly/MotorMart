using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MotorMart.Core.Models.Validation.DataAnnotations
{
    public class NonAmbiguousAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "Your search is too ambiguous. Please use words other than words such as 'and' and 'the'.";
        private readonly object _typeId = new object();

        public bool Required { get; set; }

        public NonAmbiguousAttribute()
            : base(_defaultErrorMessage)
        {
            Required = true;
        }

        public NonAmbiguousAttribute(bool required)
            : base(_defaultErrorMessage)
        {
            Required = required;
        }

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (!Required && value.ToString().Trim() == String.Empty) return true;

                string[] disallowedWords = new string[] { "and", "the", "was", "all", "its" };

                foreach (var item in disallowedWords)
                {
                    if (item.ToUpper() == value.ToString().ToUpper().Trim())
                    {
                        return false;
                    }
                }                
            }
            return true;
        }
    }
}
