using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorMart.Web.Models
{
    public class ErrorViewModel : MasterViewModel
    {
        public Exception Exception;

        public ErrorViewModel(Exception exception)
        {
            this.Exception = exception;
        }
    }
}