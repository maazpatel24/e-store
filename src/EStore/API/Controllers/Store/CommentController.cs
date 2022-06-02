using API.Controllers.Base;
using BLL.Businesses.Base;
using DAL.Entities.Store;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.Controllers.Store
{
    public class CommentController : BaseApiController<Comment>
    {
        public CommentController(IBusiness<Comment> business, ILogger<BaseApiController<Comment>> logger, IActionContextAccessor accessor) : base(business, logger, accessor)
        {
        }
    }
}