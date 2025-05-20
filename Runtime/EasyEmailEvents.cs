using System;

namespace GAG.EasyEmail
{
    public class EasyEmailEvents
    {
        public static event Action<string, string> OnEmailSent;
        public static void RaiseOnEmailSent(string senderEmail, string attachmentPath)
        {
            OnEmailSent?.Invoke(senderEmail, attachmentPath);
        }

        public static event Action<bool, string> OnEmailStatusReceived;
        public static void RaiseOnEmailStatusReceived(bool isSuccess, string message)
        {
            OnEmailStatusReceived?.Invoke(isSuccess, message);
        }
    }
}