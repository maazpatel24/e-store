using DAL.DataContext;
using DAL.Entities.Store;
using DAL.Repositories.Base;

namespace DAL.Repositories.Store
{
    public class OrderRepository : BaseRepository<Order, DatabaseContext>
    {
        public OrderRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
