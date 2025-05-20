using System.IO;
using UnityEngine;

namespace GAG.EasyEmail
{
    public class EasyEmailManager : MonoBehaviour
    {
        [SerializeField] string _receiverEmailAddress;
        [SerializeField] string _attachmentPath = Path.Combine(Application.streamingAssetsPath, "screenshot.png");

        private void OnEnable()
        {
            EasyEmailEvents.OnEmailStatusReceived += OnEmailStatusReceived;
        }

        public void SendEmail()
        {
            EasyEmailEvents.RaiseOnEmailSent(_receiverEmailAddress, _attachmentPath);
        }

        void OnEmailStatusReceived(bool isSuccess, string message)
        {
            if (isSuccess)
            {
                //Email sent successfully!
                Debug.Log(message);
            }
            else
            {
                //Failed to send email
                Debug.LogError(message);
            }
        }
    }
}
