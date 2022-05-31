using API.Controllers.Base;
using BLL.Businesses.Base;
using DAL.Entities.Login;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.Controllers.Login
{
    public class RoleController : BaseApiController<Role>
    {
        public RoleController(IBusiness<Role> business, ILogger<BaseApiController<Role>> logger, IActionContextAccessor accessor) : base(business, logger, accessor)
        {
        }
    }
}