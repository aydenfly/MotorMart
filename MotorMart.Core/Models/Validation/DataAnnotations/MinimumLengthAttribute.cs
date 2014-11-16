using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MotorMart.Core.Models.Validation.DataAnnotations
{
    public class MinimumLengthAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "Must have at least '{0}' characters";
        private readonly object _typeId = new object();

        public int Minimum { get; private set; }
        public bool Required { get; set; }

        public MinimumLengthAttribute(int minimum)
            : base(_defaultErrorMessage)
        {
            Minimum = minimum;
            Required = true;
        }

        public MinimumLengthAttribute(int minimum, bool required)
            : base(_defaultErrorMessage)
        {
            Minimum = minimum;
            Required = required;
        }        

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString, Minimum);
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                // Not required, so don't validate any further if an empty value is provided
                if (!Required && value.ToString().Trim() == String.Empty) return true;

                if (value.ToString().Length >= Minimum)
                {
                    return true;
                }
            }

            return !Required;
        }
    }
}
