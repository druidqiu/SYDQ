using SYDQ.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Core
{
    public class EmailMessage : EntityBase
    {
        public EmailMessage()
            : this(null, null)
        {
        }

        public EmailMessage(string subject, string body)
        {
            Body = body;
            Subject = subject;
        }

        public int Id { get; set; }
        public string Body { get; set; }
        public User FromUser { get; set; }
        public int? FromUserKey { get; set; }
        public bool Sent { get; set; }
        public string Subject { get; set; }
        public User ToUser { get; set; }
        public int ToUserKey { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
