using System.Net.Mail;
using System.Net;
using UnityEngine;
using System;
using System.IO;

namespace GAG.EasyEmail
{
    public class EasyEmailHandler : MonoBehaviour
    {
        [Header("Receiver Settings")]
        [SerializeField] string _subject = "Hello";
        [SerializeField] string _body = "This is from EasyEmail";
        [SerializeField] bool _checkAttachmentPath = true;
        string _attachmentPath;
        string _receiverEmailAddress;

        [Header("Sender Settings")]
        [TextArea]
        [SerializeField]
        string NoteForAppPassword = "1. Enable 2-Step Verification: https://myaccount.google.com/security \n" +
            "2. Genarate app password: https://myaccount.google.com/apppasswords \n" +
            "3. Remove spaces";
        [Tooltip("Remove spaces")]
        [SerializeField] string _senderAppPassword;
        [SerializeField] string _senderEmailAddress;

        [Header("Email Settings")]
        [SerializeField] string _smtpClient = "smtp.gmail.com";
        [SerializeField] int _serverPort = 587;

        private void OnEnable()
        {
            EasyEmailEvents.OnEmailSent += SendEmailWithAttachment;
        }

        void SendEmailWithAttachment(string senderEmail, string attachmentPath)
        {
            _receiverEmailAddress = senderEmail;
            _attachmentPath = attachmentPath;

            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient(_smtpClient);

            try
            {
                mail.From = new MailAddress(_senderEmailAddress);
                mail.To.Add(_receiverEmailAddress);
                mail.Subject = _subject;
                mail.Body = _body;

                if (_checkAttachmentPath)
                {
                    if (string.IsNullOrEmpty(_attachmentPath) || !File.Exists(_attachmentPath))
                    {
                        EasyEmailEvents.RaiseOnEmailStatusReceived(false, $"Attachment file not found at: {_attachmentPath}");
                        return;
                    }
                    else
                    {
                        Attachment attachment = new Attachment(_attachmentPath);
                        mail.Attachments.Add(attachment);
                    }
                }

                smtpServer.Port = _serverPort;
                smtpServer.Credentials = new NetworkCredential(_senderEmailAddress, _senderAppPassword) as ICredentialsByHost;
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                EasyEmailEvents.RaiseOnEmailStatusReceived(true, "Email sent successfully.");
            }
            catch (SmtpException smtpEx)
            {
                EasyEmailEvents.RaiseOnEmailStatusReceived(false, $"SMTP Error: {smtpEx.StatusCode} - {smtpEx.Message}");
            }
            catch (FormatException formatEx)
            {
                EasyEmailEvents.RaiseOnEmailStatusReceived(false, $"Invalid email format: {formatEx.Message}");
            }
            catch (Exception ex)
            {
                EasyEmailEvents.RaiseOnEmailStatusReceived(false, $"Unexpected error: {ex.Message}");
            }
            finally
            {
                foreach (Attachment att in mail.Attachments)
                {
                    att.Dispose();
                }
                mail.Dispose();
                smtpServer.Dispose();
            }
        }
    }
}