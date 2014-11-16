using System;
using System.Web.Mvc;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorMart.Core.ActionResults
{
    public class DownloadActionResult : ActionResult
    {
        public DownloadActionResult()
        {
        }

        public DownloadActionResult(string virtualPath)
        {
            this.VirtualPath = virtualPath;
        }

        public string VirtualPath
        {
            get;
            set;
        }

        public string ContentType
        {
            get;
            set;
        }

        public string FileDownloadName
        {
            get;
            set;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            string filePath = this.VirtualPath;

            if (File.Exists(filePath))
            {
                if (!String.IsNullOrEmpty(FileDownloadName))
                {
                    context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + this.FileDownloadName);
                }

                if (!String.IsNullOrEmpty(ContentType))
                {
                    context.HttpContext.Response.ContentType = ContentType;
                }

                context.HttpContext.Response.TransmitFile(filePath);
            }
            else
            {
                context.HttpContext.Response.Write("File does not exist");
            }
        }
    }
}
