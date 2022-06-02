using DAL.Entities.Login;
using DAL.Models.Api;
using DAL.Models.Common;
using WEB.Helpers.Services.Base;
using WEB.Helpers.Services.Helpers;

namespace WEB.Helpers.Services
{
    public class UserService : BaseService<User>
    {
        public UserService(IRestClientServiceProvider restClientServiceProvider) : base(restClientServiceProvider)
        {
            if (string.IsNullOrWhiteSpace(Path))
            {
                Path = "User";
            }
        }

        public UserService(IRestClientServiceProvider restClientServiceProvider, string path) : base(restClientServiceProvider, path)
        {
            if (string.IsNullOrWhiteSpace(Path))
            {
                Path = path ?? "User";
            }
        }

        #region RegisterLogin

        public async Task<User> Authenticate(AuthenticateModel model)
        {
            var response = await _restClientServiceProvider.Post<ApiResult<User>>($"{Path}/Authenticate", model).ConfigureAwait(false);
            if (response?.Success == true)
            {
                _restClientServiceProvider.SetAuthenticator(response.Data?.Token);
                return response.Data;
            }
            else
            {
                throw new ErrorResultException(response?.Error);
            }
        }

        public async Task<User> Register(RegisterModel model)
        {
            var response = await _restClientServiceProvider.Post<ApiResult<User>>($"{Path}/Register", model).ConfigureAwait(false);
            if (response?.Success == true)
            {
                _restClientServiceProvider.SetAuthenticator(response.Data?.Token);
                return response.Data;
            }
            else
            {
                throw new ErrorResultException(response?.Error);
            }
        }

        public void Logout()
        {
            _restClientServiceProvider.SetAuthenticator(null);
        }

        #endregion RegisterLogin
    }
}