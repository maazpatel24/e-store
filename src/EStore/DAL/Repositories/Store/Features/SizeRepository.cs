using DAL.DataContext;
using DAL.Entities.Store.Features;
using DAL.Repositories.Base;

namespace DAL.Repositories.Store.Features
{
    public class SizeRepository : BaseRepository<Size, DatabaseContext>
    {
        public SizeRepository(DatabaseContext context) : base(context)
        {
        }
    }
}