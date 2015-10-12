using System;
using System.Collections.Generic;

namespace SYDQ.Infrastructure.Web.Mvc.Session
{
    [Serializable]
    public class UserInfo
    {
        public String Username { get; set; }
        public String FullName { get; set; }
        public String Email { get; set; }
        public RoleType UserRole { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime CurrentLoginTime { get; set; }
        public List<string> PermissionKeys { get; set; }

        public int Year { get; set; }
    }
}
