using System;
using System.Net.Http;

namespace ERP_Mobile.Services
{
    public interface IApiService
    {
        public HttpClient Client { get; set; }
        public void SetBaseUrl(string ip, int port);
    }
    public class ApiService : IApiService
    {
        public HttpClient Client { get; set; }
        private readonly IApiSettings _apiSettings;
        public ApiService(IApiSettings apiSettings)
        {
            _apiSettings = apiSettings;
            var (ip, port) = _apiSettings.LoadServerSettings();
            Client = new HttpClient
            {
                BaseAddress = new Uri($"http://{ip}:{port}/rerpapi/")
            };
            Client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public void SetBaseUrl(string ip, int port)
        {
            var newBaseUrl = $"http://{ip}:{port}/";
            if (string.IsNullOrWhiteSpace(newBaseUrl))
                throw new ArgumentException("Base URL cannot be empty.", nameof(newBaseUrl));
            Preferences.Set("BaseURL", newBaseUrl);
            Client = new HttpClient
            {
                BaseAddress = new Uri(newBaseUrl)
            };
        }
    }
}