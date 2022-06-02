using DAL.Entities.Login;
using WEB.Models;

namespace WEB.Helpers.Constants
{
    public static class NavLinks
    {
        public static readonly List<NavLink> Links = new List<NavLink>
        {
            new NavLink("", "Home", "Index", "Home", ""),
            new NavLink("", "Home", "Explore", "Explore", ""),
            new NavLink("", "Users", "Index", "Users", "SysAdmin"),
            new NavLink("", "Markas", "Index", "Markas", "SysAdmin"),
            new NavLink("", "Products", "Index", "Products", "SysAdmin, Admin"),
            new NavLink("", "Orders", "Index", "Orders", "SysAdmin, Admin, Customer"),
        };

        public static readonly List<NavLink> RightLinks = new List<NavLink>
        {
            new NavLink("", "Profile", "Index", "Profile", "SysAdmin, Admin, Customer"),
        };

        public static List<NavLink> GetShowable(User user)
        {
            if (user == null)
            {
                return new List<NavLink>();
            }

            return Links.Where(x =>
                string.IsNullOrWhiteSpace(x.Roles) ||
                Array.IndexOf(x.Roles.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries), user.Role.Name) > -1
            ).ToList();
        }

        public static List<NavLink> GetShowableRightLinks(User user)
        {
            if (user == null)
            {
                return new List<NavLink>();
            }

            return RightLinks.Where(x =>
                string.IsNullOrWhiteSpace(x.Roles) ||
                Array.IndexOf(x.Roles.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries), user.Role.Name) > -1
            ).ToList();
        }
    }
}
