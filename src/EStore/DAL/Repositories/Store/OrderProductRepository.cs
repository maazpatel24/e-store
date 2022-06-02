using DAL.DataContext;
using DAL.Entities.Store;
using DAL.Repositories.Base;

namespace DAL.Repositories.Store
{
    public class OrderProductRepository : BaseRepository<OrderProduct, DatabaseContext>
    {
        public OrderProductRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
