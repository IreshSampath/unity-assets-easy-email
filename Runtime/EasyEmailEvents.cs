using System;

namespace GAG.EasyEmail
{
    public class EasyEmailEvents
    {
        public static event Action<string> OnEmailSent;
        public static void RaiseOnEmailSent(string email)
        {
            OnEmailSent?.Invoke(email);
        }
    }
}
