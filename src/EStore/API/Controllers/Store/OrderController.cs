using API.Controllers.Base;
using BLL.Businesses.Base;
using DAL.Entities.Store;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.Controllers.Store
{
    public class OrderController : BaseApiController<Order>
    {
        public OrderController(IBusiness<Order> business, ILogger<BaseApiController<Order>> logger, IActionContextAccessor accessor) : base(business, logger, accessor)
        {
        }
    }
}