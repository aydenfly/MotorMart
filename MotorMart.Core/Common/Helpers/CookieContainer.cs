using System;
using System.ComponentModel;
using System.Globalization;
using System.Web;
using MotorMart.Core.Common;

namespace MotorMart.Core.Common
{
	public class CookieContainer : ICookieContainer
	{
		private readonly HttpRequestBase _request;
		private readonly HttpResponseBase _response;

		public CookieContainer()
		{
			var httpContext = new HttpContextWrapper(HttpContext.Current);
			_request = httpContext.Request;
			_response = httpContext.Response;
		}

		public CookieContainer(HttpRequestBase request, HttpResponseBase response)
		{
			Check.IsNotNull(request, "request");
			Check.IsNotNull(response, "response");

			_request = request;
			_response = response;
		}

		#region ICookieContainer Members

		public bool Exists(string key)
		{
			Check.IsNotEmpty(key, "key");

			return _request.Cookies[key] != null;
		}        

		public string GetValue(string key)
		{
			Check.IsNotEmpty(key, "key");

			HttpCookie cookie = _request.Cookies[key];
			return cookie != null ? cookie.Value : null;
		}

		public T GetValue<T>(string key)
		{
			string val = GetValue(key);

			if (val == null)
				return default(T);

			Type type = typeof (T);
			bool isNullable = type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof (Nullable<>));
			if (isNullable)
				type = new NullableConverter(type).UnderlyingType;

			return (T) Convert.ChangeType(val, type, CultureInfo.InvariantCulture);
		}

		public void SetValue(string key, object value, DateTime expires)
		{
			Check.IsNotEmpty(key, "key");

			string strValue = CheckAndConvertValue(value);

            // Avoid conflict with domain - clear old cookies
            Destroy(key);

            HttpCookie cookie = new HttpCookie(key, strValue) { Domain = GlobalSettings.CookieDomain };

            if (strValue == null)
            {
                // Value is null - expire the cookie
                cookie.Expires = DateTime.Now.AddYears(-10);
            }
            else
            {
                cookie.Expires = expires;                
            }
            _response.Cookies.Set(cookie);
		}

		#endregion

        private void Destroy(string key)
        {
            Check.IsNotEmpty(key, "key");

            HttpCookie cookie = _request.Cookies[key];

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-10);
                _response.Cookies.Set(cookie);
            }
        }

		private static string CheckAndConvertValue(object value)
		{
			if (value == null)
				return null;

			if (value is string)
				return value.ToString();

			// only allow value types and nullable<value types>

			Type type = value.GetType();
			bool isTypeAllowed = false;

			if (type.IsValueType)
				isTypeAllowed = true;
			else
			{
				bool isNullable = type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof (Nullable<>));
				if (isNullable)
				{
					NullableConverter converter = new NullableConverter(type);
					Type underlyingType = converter.UnderlyingType;
					if (underlyingType.IsValueType)
						isTypeAllowed = true;
				}
			}

			if (!isTypeAllowed)
				throw new NotSupportedException("Only value types and Nullable<ValueType> are allowed!");

			return (string) Convert.ChangeType(value, typeof (string), CultureInfo.InvariantCulture);
		}

        public static class Check
        {
            public static void IsNotNull(object argument, string argumentName)
            {
                if (argument == null)
                    throw new ArgumentNullException(argumentName);
            }

            public static void IsNotEmpty(string argument, string argumentName)
            {
                if (String.IsNullOrEmpty((argument ?? string.Empty).Trim()))
                    throw new ArgumentNullException(argumentName);
            }
        }
	}
}