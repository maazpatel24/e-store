using DAL.Entities.Store;
using WEB.Helpers.Services.Base;

namespace WEB.Helpers.Services
{
    public class MarkaService : BaseService<Marka>
    {
        public MarkaService(IRestClientServiceProvider restClientServiceProvider) : base(restClientServiceProvider)
        {
            if (string.IsNullOrWhiteSpace(Path))
            {
                Path = "Marka";
            }
        }

        public MarkaService(IRestClientServiceProvider restClientServiceProvider, string path) : base(restClientServiceProvider, path)
        {
            if (string.IsNullOrWhiteSpace(Path))
            {
                Path = path ?? "Marka";
            }
        }
    }
}
