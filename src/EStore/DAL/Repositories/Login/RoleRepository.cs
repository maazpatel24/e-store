using DAL.DataContext;
using DAL.Entities.Login;
using DAL.Repositories.Base;

namespace DAL.Repositories.Login
{
    public class RoleRepository : BaseRepository<Role, DatabaseContext>
    {
        public RoleRepository(DatabaseContext context) : base(context)
        {
        }
    }
}