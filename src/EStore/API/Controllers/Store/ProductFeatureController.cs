using API.Controllers.Base;
using BLL.Businesses.Base;
using DAL.Entities.Store;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.Controllers.Store
{
    public class ProductFeatureController : BaseApiController<ProductFeature>
    {
        public ProductFeatureController(IBusiness<ProductFeature> business, ILogger<BaseApiController<ProductFeature>> logger, IActionContextAccessor accessor) : base(business, logger, accessor)
        {
        }
    }
}