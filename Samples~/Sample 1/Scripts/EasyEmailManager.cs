using System.IO;
using UnityEngine;

namespace GAG.EasyEmail
{
    public class EasyEmailManager : MonoBehaviour
    {
        public void SendEmail()
        {
            string path = Path.Combine(Application.streamingAssetsPath, "screenshot.png");
            EasyEmailEvents.RaiseOnEmailSent(path);
        }
    }
}
