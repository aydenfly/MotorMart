using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.ComponentModel;
using System.Web.Mvc;
using System.Collections.Generic;

namespace MotorMart.Core.Models.Validation.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public sealed class RequiredWhenPropertyEqualToAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}' is set to '{1}' so please complete.";
        private readonly object _typeId = new object();

        public RequiredWhenPropertyEqualToAttribute(string property, string value)
            : base(_defaultErrorMessage)
        {
            Property = property;
            Value = value;            
        }

        public string Property { get; private set; }
        public string Value { get; private set; }        

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString, Property, Value);
        }

        public override bool IsValid(object value)
        {
            // we don't have enough information here to be able to validate against another field
            // we need the DataAnnotationsMustMatchValidator adapter to be registered
            throw new Exception("RequiredWhenPropertySetAttribute requires the DataAnnotationsRequiredWhenPropertySetAttribute adapter to be registered");
        }
    }

    public class RequiredWhenPropertyEqualToValidator : DataAnnotationsModelValidator<RequiredWhenPropertyEqualToAttribute>
    {
        public RequiredWhenPropertyEqualToValidator(ModelMetadata metadata, ControllerContext context, RequiredWhenPropertyEqualToAttribute attribute)
            : base(metadata, context, attribute)
        {

        }
        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            var propertyToMatch = Metadata.ContainerType.GetProperty(Attribute.Property);
            if (propertyToMatch != null)
            {
                var valueToMatch = propertyToMatch.GetValue(container, null);
                var thisValue = Metadata.Model;

                if (Object.Equals(valueToMatch.ToString().ToLower(), Attribute.Value.ToLower()))
                {
                    if (String.IsNullOrEmpty(thisValue.ToString().Trim()))
                    {
                        yield return new ModelValidationResult { Message = ErrorMessage };
                    }
                }                
            }

            // we're not calling base.Validate here so that the attribute IsValid method doesn't get called
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            string propertyId = GetFullHtmlFieldId(Attribute.Property);
            string propertyValue = Attribute.Value;
            yield return new ModelClientRequiredWhenPropertyEqualToValidationRule(ErrorMessage, propertyId, propertyValue);
        }

        private string GetFullHtmlFieldId(string partialFieldName)
        {
            ViewContext viewContext = (ViewContext)ControllerContext;
            return viewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(partialFieldName);            
        }
    }

    public class ModelClientRequiredWhenPropertyEqualToValidationRule : ModelClientValidationRule
    {
        public ModelClientRequiredWhenPropertyEqualToValidationRule(string errorMessage, string property, string value)
        {
            ErrorMessage = errorMessage;

            ValidationType = "requiredWhenPropertyEqual";

            ValidationParameters.Add("propertyNameToEqual", property);
            ValidationParameters.Add("propertyValue", value);
        }
    }
}
