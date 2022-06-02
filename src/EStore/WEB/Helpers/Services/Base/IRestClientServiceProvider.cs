using RestSharp;

namespace WEB.Helpers.Services.Base
{
    public interface IRestClientServiceProvider
    {
        void SetAuthenticator(string token);
        Task<T> Get<T>(string path);
        Task<T> Post<T>(string path, object data);
        Task<T> Put<T>(string path, object data);
        Task<T> Delete<T>(string path);
    }
}
