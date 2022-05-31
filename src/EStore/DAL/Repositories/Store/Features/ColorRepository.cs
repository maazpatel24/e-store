using DAL.DataContext;
using DAL.Entities.Store.Features;
using DAL.Repositories.Base;

namespace DAL.Repositories.Store.Features
{
    public class ColorRepository : BaseRepository<Color, DatabaseContext>
    {
        public ColorRepository(DatabaseContext context) : base(context)
        {
        }
    }
}