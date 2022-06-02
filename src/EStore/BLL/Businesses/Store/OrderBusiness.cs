using BLL.Businesses.Base;
using DAL.Entities.Store;
using DAL.Repositories.Base;

namespace BLL.Businesses.Store
{
    public class OrderBusiness : BaseBusiness<Order>
    {
        public OrderBusiness(IRepository<Order> repository) : base(repository)
        {
        }
    }
}
