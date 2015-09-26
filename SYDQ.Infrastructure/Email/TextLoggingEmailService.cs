using SYDQ.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Email
{
    public class TextLoggingEmailService : IEmailService
    {
        public bool SendMail(List<string> tos, List<string> ccs, List<string> bccs, List<string> attachmentFiles, PriorityLevel priorityLevel, string subject, string body)
        {
            StringBuilder email = new StringBuilder();
            string tosStr = "";
            string ccsStr = "";
            string bccsStr = "";
            string attachmentFilesStr = "";
            if (tos != null && tos.Count > 0)
            {
                tos.ForEach(t => tosStr = t + ",");
            }
            else
            {
                LoggingFactory.GetLogger().Info("mail to can not be null.");
                return false;
            }
            if (ccs != null && ccs.Count > 0)
            {
                ccs.ForEach(c => ccsStr = c + ",");
            }
            if (bccs != null && bccs.Count > 0)
            {
                bccs.ForEach(b => bccsStr = b + ",");
            }
            if (attachmentFiles != null && attachmentFiles.Count > 0)
            {
                attachmentFiles.ForEach(a => attachmentFilesStr = a + ",");
            }

            email.AppendLine(String.Format("To: {0}", tosStr));
            email.AppendLine(String.Format("CC: {0}", ccsStr.Length > 0 ? ccsStr.TrimEnd(',') : ""));
            email.AppendLine(String.Format("BCC: {0}", bccsStr.Length > 0 ? bccsStr.TrimEnd('，') : ""));
            email.AppendLine(String.Format("Attachment: {0}", attachmentFilesStr.Length > 0 ? attachmentFilesStr.TrimEnd('，') : ""));
            email.AppendLine(String.Format("Subject: {0}", subject));
            email.AppendLine(String.Format("Body: {0}", body));

            LoggingFactory.GetLogger().Info(email.ToString());

            return true;
        }
    }
}
