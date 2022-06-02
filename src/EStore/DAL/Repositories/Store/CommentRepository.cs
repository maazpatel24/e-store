using DAL.DataContext;
using DAL.Entities.Store;
using DAL.Repositories.Base;

namespace DAL.Repositories.Store
{
    public class CommentRepository : BaseRepository<Comment, DatabaseContext>
    {
        public CommentRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
