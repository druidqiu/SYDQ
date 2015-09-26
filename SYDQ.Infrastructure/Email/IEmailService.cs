using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Email
{
    public enum PriorityLevel
    {
        Normal = 0,
        Low = 1,
        High = 2,
    }

    public interface IEmailService
    {
        bool SendMail(List<string> tos, List<string> ccs, List<string> bccs, List<string> attachmentFiles, PriorityLevel priorityLevel, string subject, string body);
    }
}
