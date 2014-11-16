using System;

namespace MotorMart.Core.Common
{
	public class AppCookies : IAppCookies
	{
		private readonly ICookieContainer _cookieContainer;

		public AppCookies(ICookieContainer cookieContainer)
		{
			_cookieContainer = cookieContainer;
		}

        public int? UserAccountId
        {
            get { return _cookieContainer.GetValue<int?>("UID"); }
            set { _cookieContainer.SetValue("UID", value, DateTime.Now.AddYears(1)); }
        }

        public string UserAccountSecurityKey
        {
            get { return _cookieContainer.GetValue("UID-KEY"); }
            set { _cookieContainer.SetValue("UID-KEY", value, DateTime.Now.AddYears(1)); }
        }

		public string Preferences
		{
			get { return _cookieContainer.GetValue("APP-PREFS"); }
            set { _cookieContainer.SetValue("APP-PREFS", value, DateTime.Now.AddYears(1)); }
		}

        public DateTime? LastVisit
		{
            get { return _cookieContainer.GetValue<DateTime?>("LAST-VISIT"); }
			set { _cookieContainer.SetValue("LAST-VISIT", value, DateTime.Now.AddYears(1)); }
		}
	}
}