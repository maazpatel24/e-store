using DAL.Entities.Login;
using DAL.Models.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers.Attributes
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
            var Unauthoried = user == null;
            if (!Unauthoried && Roles?.Length > 0)
            {
                Unauthoried = !Roles.Split(',').Select(x => x.Trim()).Any(x => x == user?.Role.Name);
            }
            if (Unauthoried)
            {
                // not logged in
                context.Result = new JsonResult(new ApiResult<User>(false, null, new UnauthorizedResult().StatusCode, "Unauthorized"))
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }

        private static bool SkipAuthorization(AuthorizationFilterContext context)
        {
            if (context == null) return false;
            return context.ActionDescriptor.EndpointMetadata.Any(x => x.GetType() == typeof(AllowAnonymousAttribute));
        }
    }
}