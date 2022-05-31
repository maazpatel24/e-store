using BLL.Businesses.Base;
using DAL.Entities.Store.Features;
using DAL.Repositories.Base;

namespace BLL.Businesses.Login
{
    public class ColorBusiness : BaseBusiness<Color>
    {
        public ColorBusiness(IRepository<Color> repository) : base(repository)
        {
        }
    }
}