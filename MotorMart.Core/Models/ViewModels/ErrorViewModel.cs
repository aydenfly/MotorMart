using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorMart.Core.Models;

namespace MotorMart.Core.Models
{
    public class ErrorViewModel
    {
        public Exception Exception;

        public ErrorViewModel(Exception exception)
        {
            this.Exception = exception;
        }
    }
}