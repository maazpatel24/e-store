using DAL.Entities.Base;
using DAL.Models.Api;
using WEB.Helpers.Services.Helpers;

namespace WEB.Helpers.Services.Base
{
    public class BaseService<T> : IService<T>
        where T : BaseEntity, IEntity
    {
        protected readonly IRestClientServiceProvider _restClientServiceProvider;
        public string Path { get; set; }

        public BaseService(IRestClientServiceProvider restClientServiceProvider)
        {
            _restClientServiceProvider = restClientServiceProvider;
        }

        public BaseService(IRestClientServiceProvider restClientServiceProvider, string path)
        {
            _restClientServiceProvider = restClientServiceProvider;
            Path = path;
        }

        public async Task<T> Add(T entity)
        {
            var response = await _restClientServiceProvider.Post<ApiResult<T>>(Path, entity).ConfigureAwait(false);
            if (response?.Success == true)
            {
                return response.Data;
            }
            else
            {
                throw new ErrorResultException(response?.Error);
            }
        }

        public async Task<T> Delete(long id)
        {
            var response = await _restClientServiceProvider.Delete<ApiResult<T>>($"{Path}/{id}").ConfigureAwait(false);
            if (response?.Success == true)
            {
                return response.Data;
            }
            else
            {
                throw new ErrorResultException(response?.Error);
            }
        }

        public async Task<List<T>> Get()
        {
            var response = await _restClientServiceProvider.Get<ApiResult<List<T>>>(Path).ConfigureAwait(false);
            if (response?.Success == true)
            {
                return response?.Data;
            }
            else
            {
                throw new ErrorResultException(response?.Error);
            }
        }

        public async Task<T> Get(long id)
        {
            var response = await _restClientServiceProvider.Get<ApiResult<T>>($"{Path}/{id}").ConfigureAwait(false);
            if (response?.Success == true)
            {
                return response?.Data;
            }
            else
            {
                throw new ErrorResultException(response?.Error);
            }
        }

        public async Task<T> Update(T entity)
        {
            var response = await _restClientServiceProvider.Put<ApiResult<T>>(Path, entity).ConfigureAwait(false);
            if (response?.Success == true)
            {
                return response.Data;
            }
            else
            {
                throw new ErrorResultException(response?.Error);
            }
        }
    }
}