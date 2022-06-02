using COMN.Extensions;
using DAL.Entities.Store;
using DAL.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WEB.Helpers.Services.Base;

namespace WEB.Controllers
{
    [Helpers.Attributes.Authorize(Roles = "SysAdmin, Admin, Customer")]
    public class ProductsController : Controller
    {
        private readonly AppConfigration _appConfigration;
        private readonly IService<Product> _productService;
        private readonly IHttpContextAccessor _accessor;

        public ProductsController(IOptions<AppConfigration> options, IService<Product> userService, IHttpContextAccessor accessor)
        {
            _appConfigration = options.Value;
            _productService = userService;
            _accessor = accessor;
        }

        // GET: ProductsController
        [Helpers.Attributes.Authorize(Roles = "SysAdmin, Admin")]
        public async Task<ActionResult> Index()
        {
            var products = (await _productService.Get().ConfigureAwait(false))?.Take(30);
            foreach (var item in products)
            {
                item.Description = string.Empty;

                item.Marka.Description = string.Empty;

                //item.Features = null;
                foreach (var citem in item.Features)
                {
                    citem.Product = null;
                }

                //item.Comments = null;
                item.Comments = item.Comments?.Take(3).ToList();
                foreach (var citem in item.Comments)
                {
                    citem.Product = null;
                    citem.User = null;
                }
            }
            ViewData["Products"] = products?.Serialize();
            return View();
        }

        // GET: ProductsController/Details/5
        [Helpers.Attributes.Authorize(Roles = "SysAdmin, Admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        [Helpers.Attributes.Authorize(Roles = "SysAdmin, Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Helpers.Attributes.Authorize(Roles = "SysAdmin, Admin")]
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

        // GET: ProductsController/Edit/5
        [Helpers.Attributes.Authorize(Roles = "SysAdmin, Admin")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Helpers.Attributes.Authorize(Roles = "SysAdmin, Admin")]
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

        // GET: ProductsController/Delete/5
        [Helpers.Attributes.Authorize(Roles = "SysAdmin, Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Helpers.Attributes.Authorize(Roles = "SysAdmin, Admin")]
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