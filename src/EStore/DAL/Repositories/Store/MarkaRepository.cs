using DAL.DataContext;
using DAL.Entities.Store;
using DAL.Repositories.Base;

namespace DAL.Repositories.Store
{
    public class MarkaRepository : BaseRepository<Marka, DatabaseContext>
    {
        public MarkaRepository(DatabaseContext context) : base(context)
        {
        }
    }
}