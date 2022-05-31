using DAL.DataContext;
using DAL.Entities.Login;
using DAL.Repositories.Base;

namespace DAL.Repositories.Login
{
    public class SessionRepository : BaseRepository<Session, DatabaseContext>
    {
        public SessionRepository(DatabaseContext context) : base(context)
        {
        }
    }
}