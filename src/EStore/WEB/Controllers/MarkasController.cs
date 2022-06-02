using COMN.Extensions;
using DAL.Entities.Store;
using DAL.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WEB.Helpers.Services.Base;

namespace WEB.Controllers
{
    [Helpers.Attributes.Authorize(Roles = "SysAdmin, Admin, Customer")]
    public class MarkasController : Controller
    {
        private readonly AppConfigration _appConfigration;
        private readonly IService<Marka> _markaService;
        private readonly IHttpContextAccessor _accessor;

        public MarkasController(IOptions<AppConfigration> options, IService<Marka> userService, IHttpContextAccessor accessor)
        {
            _appConfigration = options.Value;
            _markaService = userService;
            _accessor = accessor;
        }

        // GET: MarkasController
        [Helpers.Attributes.Authorize(Roles = "SysAdmin")]
        public async Task<ActionResult> Index()
        {
            var markas = (await _markaService.Get().ConfigureAwait(false))?.Take(30);
            foreach (var item in markas)
            {
                item.Description = string.Empty;
                //item.Products = item.Products?.Take(3).ToList();
                foreach (var pruditem in item.Products)
                {
                    pruditem.Description = string.Empty;
                    if (pruditem.Marka != null)
                    {
                        pruditem.Marka.Description = string.Empty;
                    }
                    //pruditem.Features = null;
                    if (pruditem.Features?.Count > 0)
                    {
                        foreach (var citem in pruditem.Features)
                        {
                            citem.Product = null;
                        }
                    }

                    //pruditem.Comments = null;
                    if (pruditem.Comments?.Count > 0)
                    {
                        //pruditem.Comments = pruditem.Comments.Take(3).ToList();
                        foreach (var citem in pruditem.Comments)
                        {
                            citem.Product = null;
                            citem.User = null;
                        }
                    }
                }
            }
            ViewData["Markas"] = markas?.Serialize();

            return View();
        }

        // GET: MarkasController/Details/5
        [Helpers.Attributes.Authorize(Roles = "SysAdmin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MarkasController/Create
        [Helpers.Attributes.Authorize(Roles = "SysAdmin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarkasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Helpers.Attributes.Authorize(Roles = "SysAdmin")]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MarkasController/Edit/5
        [Helpers.Attributes.Authorize(Roles = "SysAdmin")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MarkasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Helpers.Attributes.Authorize(Roles = "SysAdmin")]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MarkasController/Delete/5
        [Helpers.Attributes.Authorize(Roles = "SysAdmin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MarkasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Helpers.Attributes.Authorize(Roles = "SysAdmin")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}