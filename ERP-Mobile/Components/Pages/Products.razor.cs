using ERP_Mobile.Models;
using ERP_Mobile.Models.DTO;
using ERP_Mobile.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Mobile.Components.Pages
{
    public partial class Products
    {
        [Parameter]
        [SupplyParameterFromQuery(Name = "bill-import-id")]
        public string? BillImportId { get; set; }
        [Parameter]
        [SupplyParameterFromQuery(Name = "bill-import-code")]
        public string? BillImportCode { get; set; }

        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private IJSRuntime JS { get; set; } = default!;
        [Inject] private IApiService apiService { get; set; } = default!;
        [Inject] private IAlertService alertService { get; set; } = default!;
        private List<ProductItems> products { get; set; } = [];

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    await JS.InvokeVoidAsync("toggleLoading", true);
                    await LoadData();
                    await JS.InvokeVoidAsync("toggleLoading", false);
                }
            }
            catch (Exception ex)
            {
                await JS.InvokeVoidAsync("toggleLoading", false);
                await alertService.ShowAsync("Thông báo", ex.Message, "OK");
            }
        }
        private async Task LoadData()
        {
            var token = Preferences.Get("Token", "");
            apiService.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await apiService.Client.GetAsync($"api/billimportdetail/billimportid/{BillImportId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Load dữ liệu thất bại.");
            }
            var json = await response.Content.ReadAsStringAsync();
            var productsDTO = JsonConvert.DeserializeObject<ProductItemsDTO>(json, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            products = productsDTO?.data ?? [];
            StateHasChanged();
        }
        private void OnRowClicked(int id)
        {

        }
        private void OnBrowserBack()
        {
            Nav.NavigateTo("/bill-imports");
        }
    }
}