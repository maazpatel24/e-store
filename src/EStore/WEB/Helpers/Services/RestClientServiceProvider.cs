using DAL.Models.Common;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using WEB.Helpers.Services.Base;

namespace WEB.Helpers.Services
{
    public class RestClientServiceProvider : IRestClientServiceProvider, IDisposable
    {
        protected readonly AppConfigration _appConfigration;
        protected readonly RestClient _RestClient;

        public RestClientServiceProvider(IOptions<AppConfigration> options)
        {
            _appConfigration = options.Value;

            _RestClient = new RestClient(new RestClientOptions()
                {
                    BaseUrl = new Uri(_appConfigration.ApiBaseUrlSSL),
                }
            );
            _RestClient.AcceptedContentTypes = new[] { "application/json" };
            _RestClient.UseJson(); // use json only
            _RestClient.UseSerializer<RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer>();
        }

        public void SetAuthenticator(string token)
        {
            _RestClient.Authenticator = string.IsNullOrWhiteSpace(token) ? null : new JwtAuthenticator(token);
        }

        public async Task<T> Get<T>(string path)
        {
            return await _RestClient.GetAsync<T>(new RestRequest(path)).ConfigureAwait(false);
        }

        public async Task<T> Post<T>(string path, object data)
        {
            return await _RestClient.PostAsync<T>(new RestRequest(path).AddJsonBody(data)).ConfigureAwait(false);
        }

        public async Task<T> Put<T>(string path, object data)
        {
            return await _RestClient.PutAsync<T>(new RestRequest(path).AddJsonBody(data)).ConfigureAwait(false);
        }

        public async Task<T> Delete<T>(string path)
        {
            return await _RestClient.DeleteAsync<T>(new RestRequest(path)).ConfigureAwait(false);
        }

        #region Dispose

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _RestClient?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ServiceProvider()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
