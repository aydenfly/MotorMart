using System;
using System.Web;
using MotorMart.Core.Models;

namespace MotorMart.Core.Common
{
    [Serializable]
    public sealed class SessionManager
    {
        private const string SESSION_MANAGER = "SESSION_MANAGER";
        
        public string SessionId { get; set; }        
        public int UserAccountId { get; set; }
        public string UserEmailAddress { get; set; }

        private SessionManager()
        {
            SessionId = HttpContext.Current.Session.SessionID;
        }

        public static SessionManager Current
        {
            get
            {
                HttpContext context = HttpContext.Current;
                SessionManager manager = context.Session[SESSION_MANAGER] as SessionManager;
                if (manager == null)
                {
                    manager = new SessionManager();
                    context.Session[SESSION_MANAGER] = manager;
                }
                return manager;
            }
        }

        public bool IsAuthenticated()
        {
            return UserAccountId > 0;
        }

        public void Destroy()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}