using System;

namespace MotorMart.Core.Common
{
    public interface IAppCookies
    {
        DateTime? LastVisit { get; set; }
        string Preferences { get; set; }
        int? UserAccountId { get; set; }
        string UserAccountSecurityKey { get; set; }
    }
}
