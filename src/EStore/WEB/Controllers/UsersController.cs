using COMN.Extensions;
using DAL.Entities.Login;
using DAL.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WEB.Helpers.Services;
using WEB.Helpers.Services.Base;

namespace WEB.Controllers
{
    [Helpers.Attributes.Authorize(Roles = "SysAdmin, Admin, Customer")]
    public class UsersController : Controller
    {
        private readonly AppConfigration _appConfigration;
        private readonly IService<User> _userService;
        private readonly IHttpContextAccessor _accessor;

        public UsersController(IOptions<AppConfigration> options, IService<User> userService, IHttpContextAccessor accessor)
        {
            _appConfigration = options.Value;
            _userService = userService;
            _accessor = accessor;
        }

        // GET: UsersController
        [Helpers.Attributes.Authorize(Roles = "SysAdmin")]
        public async Task<ActionResult> Index()
        {
            ViewData["Users"] = (await _userService.Get().ConfigureAwait(false))?.Serialize();
            return View();
        }

        #region RegisterLogin

        // GET: UsersController/RegisterLogin
        [Helpers.Attributes.AllowAnonymous]
        [Helpers.Attributes.OnlyAnonymous]
        public ActionResult RegisterLogin()
        {
            ViewData["Login"] = "show active";
            ViewData["Register"] = "";
            return View();
        }

        // POST: UsersController/Login
        [Helpers.Attributes.AllowAnonymous]
        [Helpers.Attributes.OnlyAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(IFormCollection collection)
        {
            ViewData["Login"] = "show active";
            ViewData["Register"] = "";
            try
            {
                var user = await (_userService as UserService)
                    .Authenticate(new AuthenticateModel { Username = collection["Username"], Password = collection["Password"] })
                    .ConfigureAwait(false);

                if (user != null)
                {
                    // Auth
                    HttpContext.Session.SetString("Authorization", $"Bearer {user.Token}"); //response.Data.Serialize().Deserialize<User>()
                    //HttpContext.Response.Cookies.Append("Authorization", $"Bearer {user.Token}");

                    //
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Error"] = "Something went wrong";
                    return View("RegisterLogin");
                }
            }
            catch (Exception exc)
            {
                ViewData["Error"] = exc.Message;
                return View("RegisterLogin");
            }
        }

        // POST: UsersController/Register
        [Helpers.Attributes.AllowAnonymous]
        [Helpers.Attributes.OnlyAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(IFormCollection collection)
        {
            ViewData["Login"] = "";
            ViewData["Register"] = "show active";
            try
            {
                var user = await (_userService as UserService)
                    .Register(new RegisterModel { Username = collection["Username"], Password = collection["Password"], RoleId = 3 })
                    .ConfigureAwait(false);

                if (user != null)
                {
                    // Auth
                    HttpContext.Session.SetString("Authorization", $"Bearer {user.Token}");
                    //HttpContext.Response.Cookies.Append("Authorization", $"Bearer {user.Token}");

                    //
                    return RedirectToAction(nameof(Index), nameof(HomeController));
                }
                else
                {
                    ViewData["Error"] = "Something went wrong";
                    return View();
                }
            }
            catch (Exception exc)
            {
                ViewData["Error"] = exc.Message;
                return View();
            }
        }

        // POST: UsersController/Logout
        [Helpers.Attributes.Authorize(Roles = "SysAdmin, Admin, Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            try
            {
                (_userService as UserService).Logout();
                HttpContext.Session.SetString("Authorization", null);
                //HttpContext.Response.Cookies.Append("Authorization", null);

                return RedirectToAction(nameof(RegisterLogin));
            }
            catch (Exception exc)
            {
                ViewData["Error"] = exc.Message;
                return Redirect(_accessor.HttpContext.Request.Headers["Referer"].ToString());
            }
        }

        // GET: UsersController/Signout
        [Helpers.Attributes.Authorize(Roles = "SysAdmin, Admin, Customer")]
        public ActionResult Signout()
        {
            try
            {
                (_userService as UserService).Logout();
                HttpContext.Session.SetString("Authorization", null);
                //HttpContext.Response.Cookies.Append("Authorization", null);

                return RedirectToAction(nameof(RegisterLogin));
            }
            catch (Exception exc)
            {
                ViewData["Error"] = exc.Message;
                return Redirect(_accessor.HttpContext.Request.Headers["Referer"].ToString());
            }
        }

        private string TokenGenerate(long id, string role, out DateTime expires)
        {
            expires = DateTime.UtcNow.AddDays(7);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appConfigration.SecretJwt);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim("id", id.ToString()), new Claim("role", role.ToString()) }),
                // new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, id.ToString()) }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion RegisterLogin

        // GET: UsersController/Details/5
        [Helpers.Attributes.Authorize(Roles = "SysAdmin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        [Helpers.Attributes.Authorize(Roles = "SysAdmin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
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

        // GET: UsersController/Edit/5
        [Helpers.Attributes.Authorize(Roles = "SysAdmin")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
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

        // GET: UsersController/Delete/5
        [Helpers.Attributes.Authorize(Roles = "SysAdmin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
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