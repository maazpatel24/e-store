using DAL.DataContext;
using DAL.Entities.Login;
using DAL.Repositories.Base;

namespace DAL.Repositories.Login
{
    public class UserRepository : BaseRepository<User, DatabaseContext>
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }
    }
}