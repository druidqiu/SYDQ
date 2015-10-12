using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using SYDQ.Infrastructure.Configuration;
using SYDQ.Infrastructure.Logging;

namespace SYDQ.Infrastructure.Email
{
    public class SmtpService : IEmailService
    {
        private readonly ILogger _logger = LoggingFactory.GetLogger();

        public bool SendMail(List<string> tos, List<string> ccs, List<string> bccs, List<string> attachmentFiles, PriorityLevel priorityLevel, string subject, string body)
        {
            string senderAddress = ApplicationSettingsFactory.GetApplicationSettings().SmtpUserAddress;
            string mailHost = ApplicationSettingsFactory.GetApplicationSettings().SmtpHost;
            string senderName = ApplicationSettingsFactory.GetApplicationSettings().SmtpUserName;
            string senderPwd = ApplicationSettingsFactory.GetApplicationSettings().SmtpUserPwd;
            MailMessage message = new MailMessage {From = new MailAddress(senderAddress, senderName, Encoding.UTF8)};
            if (tos != null && tos.Count > 0)
            {
                tos.ForEach(t => message.To.Add(new MailAddress(t)));
            }
            else
            {
                _logger.Warn("mail to can not be null.");
                return false;
            }
            if (ccs != null && ccs.Count > 0)
            {
                ccs.ForEach(c => message.CC.Add(new MailAddress(c)));
            }
            if (bccs != null && bccs.Count > 0)
            {
                bccs.ForEach(b => message.Bcc.Add(new MailAddress(b)));
            }
            if (attachmentFiles != null && attachmentFiles.Count > 0)
            {
                attachmentFiles.ForEach(a => message.Attachments.Add(new Attachment(a)));
            }
            message.IsBodyHtml = true;
            message.Priority = (MailPriority)((int)priorityLevel);
            message.Subject = subject;
            message.Body = body;

            SmtpClient smtpClient = new SmtpClient(mailHost)
            {
                Credentials = new NetworkCredential(senderAddress, senderPwd),
                Timeout = 999999
            };

            bool sendFlag = false;
            try
            {
                smtpClient.Send(message);
                sendFlag = true;
            }
            catch
            {
                try
                {
                    Thread.Sleep(500);
                    smtpClient.Send(message);
                    sendFlag = true;
                }
                catch (Exception ex)
                {
                    _logger.Error("", ex);
                }
            }

            //the mail sended
            if (sendFlag)
            {
                _logger.Info("mail send successfully.");
            }

            return sendFlag;
        }
    }
}
