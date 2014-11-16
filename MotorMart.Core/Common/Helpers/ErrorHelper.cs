using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MotorMart.Core.Common;
using MotorMart.Core.Models.Validation;

namespace MotorMart.Core.Common
{
	public static class ErrorHelper
	{
        public static void HandleError(Exception ex)
        {
            ModelStateDictionary ValidationDictionary = new ModelStateDictionary();
            ValidationDictionary.AddModelError(ex.Source, String.Format("Message:{0}{1} StackTrace:{2}", ex.Message, Environment.NewLine, ex.StackTrace));
            WriteToErrorLog(ValidationDictionary);
        }

        public static void HandleError(Exception ex, IValidationDictionary ValidationDictionary)
        {
            ValidationDictionary.AddError(ex.Source, String.Format("Message:{0}{1} StackTrace:{2}", ex.Message, Environment.NewLine, ex.StackTrace));
            WriteToErrorLog(ValidationDictionary.ModelState);
        }

        public static void WriteToErrorLog(ModelStateDictionary modelState)
        {
            string Message = String.Empty;

            Message += "Source site: " + GlobalSettings.WebRoot + Environment.NewLine;
            Message += "Source url:  " + HttpContext.Current.Request.RawUrl + Environment.NewLine + Environment.NewLine;

            foreach (string key in modelState.Keys)
            {
                if (modelState[key].Errors.Count > 0)
                {
                    foreach (var error in modelState[key].Errors)
                    {
                        Message += "Error: " + error.ErrorMessage + Environment.NewLine;
                        if (modelState[key].Value != null)
                        {
                            if (modelState[key].Value.RawValue != null)
                            {
                                Message += "Raw Value: " + modelState[key].Value.RawValue.ToString() + Environment.NewLine;
                            }
                            if (modelState[key].Value.AttemptedValue != null)
                            {
                                Message += "Attempted value: " + modelState[key].Value.AttemptedValue + Environment.NewLine;
                            }
                            Message += Environment.NewLine;
                        }
                    }
                }
            }

            if (GlobalSettings.WritetoErrorLog)
            {
                WriteToLog(GlobalSettings.ErrorLogFile, Message);
            }

            MailHelper emailHelper = new MailHelper();
            emailHelper.SendErrorEmail(Message);
        }

        public static void WriteToMailerLog(string Message)
        {
            WriteToLog(HttpContext.Current.Server.MapPath("/mailerlog.txt"), Message);
        }

        private static void WriteToLog(string logFile, string logMessage)
        {
            // Create a writer and open the file:
            StreamWriter log;

            if (!File.Exists(logFile))
            {
                log = new StreamWriter(logFile);
            }
            else
            {
                log = File.AppendText(logFile);
            }

            // Write to the file:
            log.WriteLine(DateTime.Now);
            log.WriteLine(logMessage);
            //log.WriteLine();

            // Close the stream:
            log.Close();
        }
	}
}
