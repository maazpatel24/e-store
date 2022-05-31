using API.Controllers.Base;
using BLL.Businesses.Base;
using DAL.Entities.Store;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.Controllers.Store
{
    public class ProductController : BaseApiController<Product>
    {
        public ProductController(IBusiness<Product> business, ILogger<BaseApiController<Product>> logger, IActionContextAccessor accessor) : base(business, logger, accessor)
        {
        }
    }
}