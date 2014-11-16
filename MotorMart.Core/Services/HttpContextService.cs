using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace MotorMart.Core.Services
{
    public class HttpContextService : IHttpContextService
    {
        public HttpContextBase Context
        {
            get
            {
                return new HttpContextWrapper(HttpContext.Current);
            }
        }

        public HttpRequestBase Request
        {
            get
            {
                return Context.Request;
            }
        }

        public HttpResponseBase Response
        {
            get
            {
                return Context.Response;
            }
        }

        public HttpSessionStateBase Session
        {
            get
            {
                return Context.Session;
            }
        }

        public NameValueCollection FormOrQuerystring
        {
            get
            {
                if (Request.RequestType == "POST")
                {
                    return Request.Form;
                }
                return Request.QueryString;
            }
        }

    }
}
