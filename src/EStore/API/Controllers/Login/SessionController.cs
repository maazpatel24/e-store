using API.Controllers.Base;
using BLL.Businesses.Base;
using DAL.Entities.Login;
using Microsoft.AspNetCore.Mvc.Infrastructure;
namespace API.Controllers.Login
{
    public class SessionController : BaseApiController<Session>
    {
        public SessionController(IBusiness<Session> business, ILogger<BaseApiController<Session>> logger, IActionContextAccessor accessor) : base(business, logger, accessor)
        {
        }
    }
}
