using System;
using System.Web;
using System.Collections.Specialized;

namespace MotorMart.Core.Services
{
    public interface IHttpContextService
    {
        HttpContextBase Context { get; }

        NameValueCollection FormOrQuerystring { get; }

        HttpRequestBase Request { get; }

        HttpResponseBase Response { get; }

        HttpSessionStateBase Session { get; }
    }
}
