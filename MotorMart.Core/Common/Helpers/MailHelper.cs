using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Elmah;

namespace MotorMart.Core.Common
{
    public class MailHelper
    {
        public MailHelper()
        {
        }

        public void GenerateEmail(string _sendToEmail, string _sendToDisplayName, string _sendFromEmail, string _sendFromDisplayName, string _subject, string _message)
        {
            EmailSender.Message msg = new EmailSender.Message();
            msg.Encoding = Encoding.UTF8;

            msg.To = new MailAddress(_sendToEmail, _sendToDisplayName);
            msg.Format = EmailSender.Format.Text;
            msg.From = new MailAddress(_sendFromEmail, _sendFromDisplayName);

            msg.Subject = _subject;

            StringBuilder sbBody = new StringBuilder();
            
            sbBody.Append(_message);
            
            sbBody.Replace("\\n", Environment.NewLine);

            sbBody.Replace("#clienturl#", GlobalSettings.ClientSiteUrl);

            msg.Body = sbBody.ToString();

            SendEmail(msg);
        }

        public void GenerateSMS(string _sendTo, string _sendFrom, string _subject, string _message)
        {
            MailMessage message = new MailMessage(_sendFrom, _sendTo, _subject, _message);
            
            SendSMS(message);
        }
        
        private void SendEmail(EmailSender.Message msg)
        {
            try
            {
                EmailSender.SmtpSender smtp = new EmailSender.SmtpSender();
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }
            catch (FormatException ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            catch (SmtpException ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        private void SendSMS(MailMessage msg)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = true;
                smtp.Send(msg);
            }
            catch (FormatException ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            catch (SmtpException ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        public void SendErrorEmail(string Errors)
        {
            if (GlobalSettings.SendEmailOnError)
            {
                try
                {
                    string Message = "The following errors were recorded:" + Environment.NewLine + Environment.NewLine;
                    Message += Errors;

                    EmailSender.Message msg = new EmailSender.Message();
                    msg.To = new MailAddress(GlobalSettings.ErrorEmailAddress);
                    msg.From = new MailAddress(GlobalSettings.ErrorEmailAddress, GlobalSettings.WebRoot + " Errors");
                    msg.Subject = GlobalSettings.WebRoot + " Errors";
                    msg.Body = Message;
                    SendEmail(msg);
                }
                catch (FormatException ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
                catch (SmtpException ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
        }

    }
}
