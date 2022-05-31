using COMN.Attributes;
using DAL.Entities.Base;
using DAL.Repositories.Base;
using System.Linq.Expressions;
using System.Text;

namespace BLL.Businesses.Base
{
    public abstract class BaseBusiness<TEntity> : IBusiness<TEntity>
        where TEntity : BaseEntity, IEntity
    {
        protected readonly IRepository<TEntity> _repository;

        protected BaseBusiness(IRepository<TEntity> repository)
        {
            this._repository = repository;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            return await this._repository.Add(entity).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> Delete(long id)
        {
            return await this._repository.Delete(id).ConfigureAwait(false);
        }

        public virtual async Task<List<TEntity>> DeleteRange(List<TEntity> entities)
        {
            return await this._repository.DeleteRange(entities).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> Get(long id)
        {
            return await this._repository.Get(id).ConfigureAwait(false);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await this._repository.GetAll().ConfigureAwait(false);
        }

        public virtual async Task<List<TEntity>> GetBy(string propertyName, string propertyValue)
        {
            var propertyType = typeof(TEntity).GetProperty(propertyName).PropertyType;
            var param = Expression.Parameter(typeof(TEntity));
            var condition =
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.Equal(
                        Expression.Property(param, propertyName),
                        Expression.Constant(Convert.ChangeType(propertyValue, propertyType), propertyType)
                    ),
                    param
                );
            //.Compile(); // for LINQ to SQl/Entities skip Compile() call
            return await this._repository.GetBy(condition).ConfigureAwait(false);
        }

        public virtual async Task<List<TEntity>> GetBy(string property1Name, string property1Value, string property2Name, string property2Value)
        {
            var property1Type = typeof(TEntity).GetProperty(property1Name).PropertyType;
            var property2Type = typeof(TEntity).GetProperty(property2Name).PropertyType;
            var param = Expression.Parameter(typeof(TEntity));
            var condition =
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.And(
                        Expression.Equal(
                            Expression.Property(param, property1Name),
                            Expression.Constant(Convert.ChangeType(property1Value, property1Type), property1Type)
                        ),
                        Expression.Equal(
                            Expression.Property(param, property2Name),
                            Expression.Constant(Convert.ChangeType(property2Value, property2Type), property2Type)
                        )
                    ),
                    param
                );
            //.Compile(); // for LINQ to SQl/Entities skip Compile() call
            return await this._repository.GetBy(condition).ConfigureAwait(false);
        }

        public virtual async Task<List<TEntity>> GetBy(Expression<Func<TEntity, bool>> condition)
        {
            return await this._repository.GetBy(condition).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            return await this._repository.Update(entity).ConfigureAwait(false);
        }

    }
}