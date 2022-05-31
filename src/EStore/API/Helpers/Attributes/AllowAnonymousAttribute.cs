using Microsoft.AspNetCore.Mvc.Authorization;

namespace API.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute, IAllowAnonymousFilter
    {
    }
}