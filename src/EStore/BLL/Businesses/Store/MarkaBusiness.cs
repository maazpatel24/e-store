using BLL.Businesses.Base;
using DAL.Entities.Store;
using DAL.Repositories.Base;

namespace BLL.Businesses.Store
{
    public class MarkaBusiness : BaseBusiness<Marka>
    {
        public MarkaBusiness(IRepository<Marka> repository) : base(repository)
        {
        }
    }
}