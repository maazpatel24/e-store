using API.Controllers.Base;
using BLL.Businesses.Base;
using DAL.Entities.Store.Features;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.Controllers.Store.Features
{
    [Helpers.Attributes.AllowAnonymous]
    public class SizeController : BaseApiController<Size>
    {
        public SizeController(IBusiness<Size> business, ILogger<BaseApiController<Size>> logger, IActionContextAccessor accessor) : base(business, logger, accessor)
        {
        }
    }
}