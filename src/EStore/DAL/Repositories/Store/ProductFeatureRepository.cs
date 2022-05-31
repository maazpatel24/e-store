using DAL.DataContext;
using DAL.Entities.Store;
using DAL.Repositories.Base;

namespace DAL.Repositories.Store
{
    public class ProductFeatureRepository : BaseRepository<ProductFeature, DatabaseContext>
    {
        public ProductFeatureRepository(DatabaseContext context) : base(context)
        {
        }
    }
}