using ERP_Mobile.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Mobile.Components.Pages
{
    public partial class Settings
    {
        [Inject] private IJSRuntime JS { get; set; } = default!;
        [Inject] private IAlertService alertService { get; set; } = default!;
        [Inject] private IApiSettings apiSettingService { get; set; } = default!;
        [Inject] private IApiService apiService { get; set; } = default!;
        private string address { get; set; } = "";
        private ElementReference addressInput;
        private ElementReference saveButton;

        protected override Task OnInitializedAsync()
        {
            address = apiSettingService.LoadServerSettings();
            return Task.CompletedTask;
        }

        public async Task OnInputKeyUp(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Key == "Enter" || e.Code == "NumpadEnter")
            {
                await JS.InvokeVoidAsync("setFocus", saveButton);
            }
        }

        public async Task OnSave()
        {
            apiSettingService.SaveServerSettings(address);
            apiService.SetBaseUrl(address);
            await alertService.ShowAsync("Thông báo", "Thông tin đã được lưu lại.", "OK");
            await JS.InvokeVoidAsync("history.back");
        }
    }
}