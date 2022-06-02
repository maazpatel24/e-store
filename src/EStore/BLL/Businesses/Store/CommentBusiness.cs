using BLL.Businesses.Base;
using DAL.Entities.Store;
using DAL.Repositories.Base;

namespace BLL.Businesses.Store
{
    public class CommentBusiness : BaseBusiness<Comment>
    {
        public CommentBusiness(IRepository<Comment> repository) : base(repository)
        {
        }
    }
}
