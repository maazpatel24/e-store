using DAL.Models.Api;
using DAL.Entities.Base;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Base
{
    public interface IApiController<TEntity>
        where TEntity : BaseEntity, IEntity
    {
        Task<ActionResult<ApiResult<TEntity>>> Delete(long id);

        Task<ActionResult<ApiResult<TEntity>>> Get();

        Task<ActionResult<ApiResult<TEntity>>> Get(long id);

        Task<ActionResult<ApiResult<TEntity>>> GetBy(string propertyName, string propertyValue);

        Task<ActionResult<ApiResult<TEntity>>> GetBy(string propertyName, string propertyValue, string property2Name, string property2Value);

        Task<ActionResult<ApiResult<TEntity>>> Post(TEntity entity);

        Task<ActionResult<ApiResult<TEntity>>> Put(TEntity entity);
    }
}