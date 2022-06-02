using DAL.Entities.Store;
using WEB.Helpers.Services.Base;

namespace WEB.Helpers.Services
{
    public class ProductService : BaseService<Product>
    {
        public ProductService(IRestClientServiceProvider restClientServiceProvider) : base(restClientServiceProvider)
        {
            if (string.IsNullOrWhiteSpace(Path))
            {
                Path = "Product";
            }
        }

        public ProductService(IRestClientServiceProvider restClientServiceProvider, string path) : base(restClientServiceProvider, path)
        {
            if (string.IsNullOrWhiteSpace(Path))
            {
                Path = path ?? "Product";
            }
        }
    }
}
