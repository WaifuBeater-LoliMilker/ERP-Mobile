using System;
using System.Net.Http;

namespace ERP_Mobile.Services
{
    public interface IApiService
    {
        public HttpClient Client { get; set; }
        public void SetBaseUrl(string address);
    }
    public class ApiService : IApiService
    {
        public HttpClient Client { get; set; }
        private readonly IApiSettings _apiSettings;
        public ApiService(IApiSettings apiSettings)
        {
            _apiSettings = apiSettings;
            var address = _apiSettings.LoadServerSettings();
            Client = new HttpClient
            {
                BaseAddress = new Uri(address)
            };
            Client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public void SetBaseUrl(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Base URL cannot be empty.", nameof(address));
            Client = new HttpClient
            {
                BaseAddress = new Uri(address)
            };
        }
    }
}