using DAL.Entities.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WEB.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Gets or sets a comma delimited list of roles that are allowed to access the resource.
        /// </summary>
        public string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (SkipAuthorization(context)) return;

            var user = context.HttpContext.Items["User"] as User;

            var authenticated = user != null;

            var authorized = false;

            if (authenticated && Roles?.Length > 0)
            {
                authorized = Roles.Split(',').Select(x => x.Trim()).Any(x => x == user?.Role.Name);
            }

            if (!authenticated)
            {
                context.Result = new ForbidResult(); 
                // new RedirectResult("/Errors/403"); 
                // new RedirectResult(context.HttpContext.Request.Headers["Referer"].ToString());
            }
            else if (!authorized)
            {
                context.Result = new UnauthorizedResult(); 
                // new RedirectResult("/Errors/401"); 
                // new UnauthorizedResult(); 
            }
        }

        private static bool SkipAuthorization(AuthorizationFilterContext context)
        {
            if (context == null) return false;
            return context.ActionDescriptor.EndpointMetadata.Any(x => x.GetType() == typeof(AllowAnonymousAttribute));
        }
    }
}