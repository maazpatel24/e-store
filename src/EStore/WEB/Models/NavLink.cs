namespace WEB.Models
{
    public class NavLink
    {
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Title { get; set; }
        public string Roles { get; set; }

        public NavLink()
        {
        }

        public NavLink(string area, string controller, string action, string title, string roles)
        {
            Area = area;
            Controller = controller;
            Action = action;
            Title = title;
            Roles = roles;
        }
    }
}