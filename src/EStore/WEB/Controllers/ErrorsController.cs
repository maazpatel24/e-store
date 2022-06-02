using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class ErrorsController : Controller
    {
        [HttpGet("[Controller]/{code}")]
        public IActionResult Index(int code)
        {
            ViewData["Code"] = code;

            return View();
        }
    }
}
