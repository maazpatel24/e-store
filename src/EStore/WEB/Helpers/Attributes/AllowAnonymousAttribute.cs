using Microsoft.AspNetCore.Mvc.Authorization;

namespace WEB.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute, IAllowAnonymousFilter
    {
    }
}