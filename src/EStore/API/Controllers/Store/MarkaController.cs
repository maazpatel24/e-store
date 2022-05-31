using API.Controllers.Base;
using BLL.Businesses.Base;
using DAL.Entities.Store;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.Controllers.Store
{
    public class MarkaController : BaseApiController<Marka>
    {
        public MarkaController(IBusiness<Marka> business, ILogger<BaseApiController<Marka>> logger, IActionContextAccessor accessor) : base(business, logger, accessor)
        {
        }
    }
}