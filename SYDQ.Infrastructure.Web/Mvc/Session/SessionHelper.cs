using System.Collections.Generic;
using System.Web;

namespace SYDQ.Infrastructure.Web.Mvc.Session
{
    public enum SessionType
    {
        UserInfo = 0,
        UserMenu = 1,
        PagePermission = 2,
    }

    public class SessionHelper
    {

        public static void StoreUserInfo(UserInfo userInfo)
        {
            HttpContext.Current.Session[SessionType.UserInfo.ToString()] = userInfo;
        }

        public static UserInfo RetrieveUserInfo()
        {
            if (HttpContext.Current.Session == null)
                return null;
            object obj = HttpContext.Current.Session[SessionType.UserInfo.ToString()];
            return obj as UserInfo;
        }

        public static void StoreUserMenu(List<Menu> menus)
        {
            HttpContext.Current.Session[SessionType.UserMenu.ToString()] = menus;
        }

        public static List<Menu> RetrieveUserMenu()
        {
            if (HttpContext.Current.Session == null)
                return null;
            object obj = HttpContext.Current.Session[SessionType.UserMenu.ToString()];
            return obj as List<Menu>;
        }

        public static bool CheckSessionValid()
        {
            var userInfo = RetrieveUserInfo();
            var userMenu = RetrieveUserMenu();
            return (userInfo != null && userMenu != null);
        }

        public static void EmptySession()
        {
            HttpContext.Current.Session[SessionType.UserInfo.ToString()] = null;
            HttpContext.Current.Session[SessionType.UserMenu.ToString()] = null;
        }

        public static RoleType GetUserRole()
        {
            var userInfo = RetrieveUserInfo();
            return userInfo == null ? RoleType.None : userInfo.UserRole;
        }

        public static List<string> GetUserPermissions()
        {
            var userInfo = RetrieveUserInfo();
            if (userInfo == null)
                return null;
            else
                return userInfo.PermissionKeys;
        }
    }
}
