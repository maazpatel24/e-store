using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    [Helpers.Attributes.Authorize(Roles = "SysAdmin, Admin, Customer")]
    public class ProfileController : Controller
    {
        // GET: ProfileController
        public ActionResult Index()
        {
            return View();
        }

    }
}
