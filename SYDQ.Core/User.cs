﻿using System;
using System.Collections.Generic;
using SYDQ.Infrastructure.Domain;

namespace SYDQ.Core
{
    public class User : EntityBase, IAggregateRoot
    {
        public User()
            : this(null)
        { }

        public User(string username)
        {
            Roles = new HashSet<Role>();
            Messages = new HashSet<EmailMessage>();
            Username = username;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpirationDate { get; set; }
        public DateTime CreatedUtc { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<EmailMessage> Messages { get; set; }

        public override string ToString()
        {
            return string.Format("userId: {0}, username: {1}, email: {2}", Id, Username, EmailAddress);
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
