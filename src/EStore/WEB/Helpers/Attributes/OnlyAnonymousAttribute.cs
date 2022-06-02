using DAL.Entities.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WEB.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OnlyAnonymousAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _onlyAnonymousPathes = new[]
        {
            "/Users/RegisterLogin",
            "/Users/Login",
            "/Users/Register"
        };

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (SkipAuthorization(context)) return;

            var user = context.HttpContext.Items["User"] as User;

            var authenticated = user != null;

            
            if (authenticated)
            {
                if (Array.IndexOf(_onlyAnonymousPathes, context.HttpContext.Request.Path.Value) > -1)
                {
                    context.Result = new RedirectResult("/Home/Index");
                }
            }
        }

        private static bool SkipAuthorization(AuthorizationFilterContext context)
        {
            if (context == null) return false;
            return context.ActionDescriptor.EndpointMetadata.Any(x => x.GetType() == typeof(AllowAnonymousAttribute));
        }
    }
}