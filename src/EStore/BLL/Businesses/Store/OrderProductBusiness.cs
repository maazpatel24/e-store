using BLL.Businesses.Base;
using DAL.Entities.Store;
using DAL.Repositories.Base;

namespace BLL.Businesses.Store
{
    public class OrderProductBusiness : BaseBusiness<OrderProduct>
    {
        public OrderProductBusiness(IRepository<OrderProduct> repository) : base(repository)
        {
        }
    }
}
