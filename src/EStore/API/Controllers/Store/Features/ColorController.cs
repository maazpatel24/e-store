using API.Controllers.Base;
using BLL.Businesses.Base;
using DAL.Entities.Store.Features;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.Controllers.Store.Features
{
    [Helpers.Attributes.AllowAnonymous]
    public class ColorController : BaseApiController<Color>
    {
        public ColorController(IBusiness<Color> business, ILogger<BaseApiController<Color>> logger, IActionContextAccessor accessor) : base(business, logger, accessor)
        {
        }
    }
}