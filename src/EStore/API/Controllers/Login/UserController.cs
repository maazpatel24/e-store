using API.Controllers.Base;
using BLL.Businesses.Base;
using DAL.Entities.Login;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BLL.Businesses;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using DAL.Models.Common;
using DAL.Models.Api;
using BLL.Businesses.Login;

namespace API.Controllers.Login
{
    public class UserController : BaseApiController<User>
    {
        private readonly AppConfigration _appConfigration;
        private readonly IBusiness<Session> _sessionBusiness;
        public UserController(IBusiness<User> business, ILogger<BaseApiController<User>> logger, IActionContextAccessor accessor, IOptions<AppConfigration> options, IBusiness<Session> sessionBusiness) : base(business, logger, accessor)
        {
            _appConfigration = options.Value;
            _sessionBusiness = sessionBusiness;
        }

        [Helpers.Attributes.AllowAnonymous]
        // POST: api/[controller]/Register
        [HttpPost("Register")]
        public async Task<ActionResult<ApiResult<User>>> PostRegister([FromBody] RegisterModel model)
        {
            this._logger.LogInformation($"[PostRegister] [{this._ip}] {JsonConvert.SerializeObject(model)}");
            var entity = await (this._business as UserBusiness).Register(model).ConfigureAwait(false);
            if (entity != null)
            {
                return Ok(new ApiResult<User>(true, null, null));
            }
            return this.BadRequestApi();
        }

        [Helpers.Attributes.AllowAnonymous]
        // POST: api/[controller]/Authenticate
        [HttpPost("Authenticate")]
        public async Task<ActionResult<ApiResult<User>>> PostAuthenticate([FromBody] AuthenticateModel model)
        {
            this._logger.LogInformation($"[PostAuthenticate] [{this._ip}] {JsonConvert.SerializeObject(model)}");
            var entity = await (this._business as UserBusiness).Authenticate(model).ConfigureAwait(false);
            if (entity != null)
            {
                entity.PasswordHash = null;
                entity.PasswordSalt = null;
                entity.Token = TokenGenerate(entity.Id, entity.Role.Name, out DateTime expires);
                try
                {
                    await this._sessionBusiness.Add(new Session { UserId = entity.Id, Token = entity.Token, Expires = expires }).ConfigureAwait(false);
                }
                catch (Exception exc)
                {
                }
                return Ok(new ApiResult<User>(true, entity, null));
            }
            return this.BadRequestApi();
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
    }
}