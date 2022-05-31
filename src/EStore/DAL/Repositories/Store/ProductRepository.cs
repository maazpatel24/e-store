using DAL.DataContext;
using DAL.Entities.Store;
using DAL.Repositories.Base;

namespace DAL.Repositories.Store
{
    public class ProductRepository : BaseRepository<Product, DatabaseContext>
    {
        public ProductRepository(DatabaseContext context) : base(context)
        {
        }
    }
}