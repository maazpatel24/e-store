using DAL.Entities.Base;

namespace WEB.Helpers.Services.Base
{
    public interface IService<T>
        where T : BaseEntity, IEntity
    {
        Task<List<T>> Get();
        Task<T> Get(long id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(long id);
    }
}
