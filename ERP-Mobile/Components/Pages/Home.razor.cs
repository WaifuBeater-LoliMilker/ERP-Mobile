using ERP_Mobile.Models;
using ERP_Mobile.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Mobile.Components.Pages
{
    public partial class Home
    {
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private IJSRuntime JS { get; set; } = default!;
        [Inject] private IApiService apiService { get; set; } = default!;
        [Inject] private IAlertService alertService { get; set; } = default!;
        private string username { get; set; } = "";
        private string password { get; set; } = "";
        private ElementReference usernameInput;
        private ElementReference passwordInput;

        public async Task OnUsernameKeyUp(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Key == "Enter" || e.Code == "NumpadEnter")
            {
                await JS.InvokeVoidAsync("setFocus", passwordInput);
            }
        }
        public async Task OnPasswordKeyUp(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Key == "Enter" || e.Code == "NumpadEnter")
            {
                await OnSubmit();
            }
        }
        public async Task OnSubmit()
        {
            try
            {
                await JS.InvokeVoidAsync("toggleLoading", true);
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    throw new Exception("Please enter username/password");
                var payload = new
                {
                    LoginName = username,
                    PasswordHash = password
                };
                var serialized = JsonConvert.SerializeObject(payload);
                var jsonContent = new StringContent(serialized,
                    Encoding.UTF8,
                    "application/json");
                var response = await apiService.Client.PostAsync($"api/api/home/login", jsonContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                var json = await response.Content.ReadAsStringAsync();
                var accountInfo = JsonConvert.DeserializeObject<AccountInfo>(json);
                Preferences.Set("Token", accountInfo!.access_token);
                await JS.InvokeVoidAsync("toggleLoading", false);
                Nav.NavigateTo($"/menu");
            }
            catch
            {
                await JS.InvokeVoidAsync("toggleLoading", false);
                await alertService.ShowAsync("Notice", "Username or password is incorrect", "OK");
            }
        }
    }
}
