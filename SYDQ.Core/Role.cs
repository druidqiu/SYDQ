using System;
using System.Collections.Generic;
using SYDQ.Infrastructure.Domain;

namespace SYDQ.Core
{
    public class Role : EntityBase
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
