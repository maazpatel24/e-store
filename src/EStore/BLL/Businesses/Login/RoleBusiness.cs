using BLL.Businesses.Base;
using DAL.Entities.Login;
using DAL.Repositories.Base;

namespace BLL.Businesses.Login
{
    public class RoleBusiness : BaseBusiness<Role>
    {
        public RoleBusiness(IRepository<Role> repository) : base(repository)
        {
        }
    }
}