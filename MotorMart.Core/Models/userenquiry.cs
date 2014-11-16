using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Linq;

namespace MotorMart.Core.Models
{
    public partial class userenquiry : IDataErrorInfo
    {
        private Dictionary<string, string> _errors = new Dictionary<string, string>();

        partial void OnCreated()
        { 
        }

        partial void OnValidate(ChangeAction action)
        {
            if (action == ChangeAction.Insert)
            {   
                datesubmitted = DateTime.Now;
            }
        }

        partial void OnmessageChanging(string value)
        {
            if (String.IsNullOrEmpty(value))
                _errors.Add("message", "Message is required");
        }
       
        #region IDataErrorInfo Members

        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (_errors.ContainsKey(columnName))
                    return _errors[columnName];
                return string.Empty;
            }
        }

        #endregion
    }
}