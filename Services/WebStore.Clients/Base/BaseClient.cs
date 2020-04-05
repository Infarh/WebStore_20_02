using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using WebStore.Domain;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected readonly HttpClient _Client;

        protected readonly string _ServiceAddress;

        protected BaseClient(IConfiguration config, string ServiceAddress)
        {
            _ServiceAddress = ServiceAddress;

            _Client = new HttpClient
            {
                BaseAddress = new Uri(config["WebApiURL"])
            };

            var headers = _Client.DefaultRequestHeaders.Accept;
            headers.Clear();
            headers.Add(new MediaTypeWithQualityHeaderValue(MimeType.Json));
        }

        private string Check(string url) => url?.StartsWith(_ServiceAddress, StringComparison.OrdinalIgnoreCase) == true
            ? url
            : url?.Length > 0 ? $"{_ServiceAddress}{(url[0] == '/' ? null : "/")}{url}" : _ServiceAddress;

        protected T GetById<T>(int id) where T : new() => Get<T>(id.ToString());

        protected T Get<T>(string url = null) where T : new() => GetAsync<T>(Check(url)).Result;

        protected async Task<T> GetByIdAsync<T>(int id, CancellationToken Cancel = default) where T : new() => await GetAsync<T>(id.ToString(), Cancel);

        protected async Task<T> GetAsync<T>(string url = null, CancellationToken Cancel = default)
            where T : new()
        {
            var response = await _Client.GetAsync(Check(url), Cancel);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<T>(Cancel);

            return new T();
        }

        protected HttpResponseMessage PostById<T>(int id, T item) => Post(id.ToString(), item);

        protected HttpResponseMessage Post<T>(T item) => Post(_ServiceAddress, item);

        protected HttpResponseMessage Post<T>(string url, T item) => PostAsync(url, item).Result;

        protected async Task<HttpResponseMessage> PostByIdAsync<T>(int id, T item, CancellationToken Cancel = default) => await PostAsync(id.ToString(), item, Cancel);

        protected async Task<HttpResponseMessage> PostAsync<T>(T item, CancellationToken Cancel = default) => await PostAsync(_ServiceAddress, item, Cancel);

        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken Cancel = default)
        {
            var response = await _Client.PostAsJsonAsync(Check(url), item, Cancel);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage PutById<T>(int id, T item) => PutAsync(id.ToString(), item).Result;
        protected HttpResponseMessage Put<T>(T item) => PutAsync(_ServiceAddress, item).Result;
        protected HttpResponseMessage Put<T>(string url, T item) => PutAsync(url, item).Result;

        protected async Task<HttpResponseMessage> PutByIdAsync<T>(int id, T item, CancellationToken Cancel = default) => await PutAsync(id.ToString(), item, Cancel);
        protected async Task<HttpResponseMessage> PutAsync<T>(T item, CancellationToken Cancel = default) => await PutAsync(_ServiceAddress, item, Cancel);
        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken Cancel = default)
        {
            var response = await _Client.PutAsJsonAsync(Check(url), item, Cancel);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage DeleteById(int id) => DeleteAsync(id.ToString()).Result;
        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;

        protected async Task<HttpResponseMessage> DeleteByIdAsync(int id, CancellationToken Cancel = default) => await DeleteAsync(id.ToString(), Cancel);
        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken Cancel = default)
        {
            return await _Client.DeleteAsync(Check(url), Cancel);
        }

        #region IDisposable

        //~BaseClient() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _Disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_Disposed || !disposing) return;
            _Disposed = true;
            _Client.Dispose();
        } 

        #endregion
    }
}
