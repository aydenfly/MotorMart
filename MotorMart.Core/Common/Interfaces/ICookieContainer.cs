using System;

namespace MotorMart.Core.Common
{
    public interface ICookieContainer
    {
        bool Exists(string key);
        string GetValue(string key);
        T GetValue<T>(string key);
        void SetValue(string key, object value, DateTime expires);
    }
}
