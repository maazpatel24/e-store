using BLL.Businesses.Base;
using DAL.Entities.Store.Features;
using DAL.Repositories.Base;

namespace BLL.Businesses.Login
{
    public class SizeBusiness : BaseBusiness<Size>
    {
        public SizeBusiness(IRepository<Size> repository) : base(repository)
        {
        }
    }
}