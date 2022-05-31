using BLL.Businesses.Base;
using DAL.Entities.Store;
using DAL.Repositories.Base;

namespace BLL.Businesses.Login
{
    public class ProductFeatureBusiness : BaseBusiness<ProductFeature>
    {
        public ProductFeatureBusiness(IRepository<ProductFeature> repository) : base(repository)
        {
        }
    }
}