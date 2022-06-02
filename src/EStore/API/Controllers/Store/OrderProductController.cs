using API.Controllers.Base;
using BLL.Businesses.Base;
using DAL.Entities.Store;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.Controllers.Store
{
    public class OrderProductController : BaseApiController<OrderProduct>
    {
        public OrderProductController(IBusiness<OrderProduct> business, ILogger<BaseApiController<OrderProduct>> logger, IActionContextAccessor accessor) : base(business, logger, accessor)
        {
        }
    }
}