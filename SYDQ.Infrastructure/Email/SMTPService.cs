using SYDQ.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Email
{
    public class SMTPService : IEmailService
    {
        protected ILogger _logger = LoggingFactory.GetLogger();
        protected string str = "";

        public bool SendMail(List<string> tos, List<string> ccs, List<string> bccs, List<string> attachmentFiles, PriorityLevel priorityLevel, string subject, string body)
        {
            bool sendFlag = false;
            string senderAddress = Configuration.ApplicationSettingsFactory.GetApplicationSettings().SmtpUserAddress; ;
            string mailHost = Configuration.ApplicationSettingsFactory.GetApplicationSettings().SmtpHost;
            string senderName = Configuration.ApplicationSettingsFactory.GetApplicationSettings().SmtpUserName;
            string senderPwd = Configuration.ApplicationSettingsFactory.GetApplicationSettings().SmtpUserPwd;
            MailMessage message = new MailMessage();
            message.From = new MailAddress(senderAddress, senderName, Encoding.UTF8);
            if (tos != null && tos.Count > 0)
            {
                tos.ForEach(t => message.To.Add(new MailAddress(t)));
            }
            else
            {
                _logger.Warn("mail to can not be null.");
                return sendFlag;
            }
            if (ccs != null && ccs.Count > 0)
            {
                ccs.ForEach(c => message.CC.Add(new MailAddress(c)));
            }
            if (bccs != null && ccs.Count > 0)
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

            SmtpClient smtpClient = new SmtpClient(mailHost);
            smtpClient.Credentials = new NetworkCredential(senderAddress, senderPwd);
            smtpClient.Timeout = 999999;

            try
            {
                smtpClient.Send(message);
                sendFlag = true;
            }
            catch
            {
                try
                {
                    System.Threading.Thread.Sleep(500);
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
