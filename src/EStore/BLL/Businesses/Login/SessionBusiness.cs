using BLL.Businesses.Base;
using DAL.Entities.Login;
using DAL.Repositories.Base;

namespace BLL.Businesses.Login
{
    public class SessionBusiness : BaseBusiness<Session>
    {
        public SessionBusiness(IRepository<Session> repository) : base(repository)
        {
        }
    }
}