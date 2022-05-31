using DAL.Entities.Base;
using System.Linq.Expressions;

namespace BLL.Businesses.Base
{
    public interface IBusiness<TEntity>
        where TEntity : BaseEntity, IEntity
    {
        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Delete(long id);

        Task<List<TEntity>> DeleteRange(List<TEntity> entities);

        Task<TEntity> Get(long id);

        Task<List<TEntity>> GetAll();

        Task<List<TEntity>> GetBy(string propertyName, string propertyValue);

        Task<List<TEntity>> GetBy(string property1Name, string property1Value, string property2Name, string property2Value);

        Task<List<TEntity>> GetBy(Expression<Func<TEntity, bool>> condition);

        Task<TEntity> Update(TEntity entity);
    }
}