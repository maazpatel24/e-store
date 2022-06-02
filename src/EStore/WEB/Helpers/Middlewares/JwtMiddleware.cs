using BLL.Businesses.Base;
using DAL.Entities.Login;
using DAL.Models.Api;
using DAL.Models.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using RestSharp.Authenticators;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WEB.Helpers.Services.Base;
using WEB.Helpers.Services.Helpers;

namespace WEB.Helpers.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppConfigration _appConfigration;
        private readonly IService<User> _userService;

        public JwtMiddleware(RequestDelegate next, IOptions<AppConfigration> appConfigration, IService<User> userService)
        {
            _next = next;
            _appConfigration = appConfigration.Value;
            _userService = userService;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrWhiteSpace(token))
            {
                token = context.Session.GetString("Authorization")?.Split(" ").Last();
                if (string.IsNullOrWhiteSpace(token))
                {
                    token = context.Request.Cookies["Authorization"]?.Split(" ").Last();
                }
            }

            if (token != null)
                await attachUserToContext(context, token);

            await _next(context);
        }

        private async Task attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appConfigration.SecretJwt);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // attach user to context on successful jwt validation
                context.Items["User"] = await _userService.Get(userId).ConfigureAwait(false);
            }
            catch(ErrorResultException cex)
            {
                Console.WriteLine(cex.Error.Message);
            }
            catch (Exception ex)
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
                Console.WriteLine(ex.Message);
            }
        }
    }
}