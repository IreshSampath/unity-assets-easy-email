using System.Net.Mail;
using System.Net;
using UnityEngine;
using System.IO;

namespace GAG.EasyEmail
{
    public class EasyEmailHandler : MonoBehaviour
    {
        [Header("Email Settings")]
        [SerializeField] string _receiverEmailAddress;
        [SerializeField] string _subject = "UI Capture";
        [SerializeField] string _body = "Here is the UI capture.";
        [Header("Sender Settings")]

        [TextArea]
        [SerializeField] string NoteForAppPassword = "1. Enable 2-Step Verification: https://myaccount.google.com/security \n" +
            "2. Genarate app password: https://myaccount.google.com/apppasswords \n" +
            "3. Remove spaces";
        [Tooltip("Remove spaces")]
        [SerializeField] string _senderAppPassword;
        [SerializeField] string _smtpClient = "smtp.gmail.com";
        [SerializeField] string _senderEmailAddress;

        private void OnEnable()
        {
            EasyEmailEvents.OnEmailSent += SendEmailWithAttachment;
        }


        void SendEmailWithAttachment(string filePath)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_senderEmailAddress);
            mail.To.Add(_receiverEmailAddress);
            mail.Subject = _subject;
            mail.Body = _body;
            mail.Attachments.Add(new Attachment(filePath));

            SmtpClient smtpServer = new SmtpClient(_smtpClient);
            smtpServer.Port = 587;

            // Enable 2-Step Verification: https://myaccount.google.com/security
            //Genarate app password: https://myaccount.google.com/apppasswords
            smtpServer.Credentials = new NetworkCredential(_senderEmailAddress, _senderAppPassword) as ICredentialsByHost;
            smtpServer.EnableSsl = true;

            smtpServer.Send(mail);
            Debug.Log("Email sent!");
        }
    }
}