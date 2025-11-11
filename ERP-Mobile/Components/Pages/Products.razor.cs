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

        private readonly CancellationTokenSource _cts = new ();
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private IJSRuntime JS { get; set; } = default!;
        [Inject] private IApiService apiService { get; set; } = default!;
        [Inject] private IAlertService alertService { get; set; } = default!;
        [Inject] private IProductSerialDataService productDataService { get; set; } = default!;
        private List<BillImportDetails> products { get; set; } = [];
        private int[] productSerialStatuses { get; set; } = [];
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
            var billImportDetailsDTO = JsonConvert.DeserializeObject<BillImportDetailsDTO>(json, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            products = billImportDetailsDTO?.data ?? [];
            productSerialStatuses = new int[products.Count];
            for (int i = 0; i < products.Count; i++)
            {
                var product = products[i];
                await Task.Run(async () =>
                {
                    productSerialStatuses[i] = await productDataService.SaveData(product);
                }, _cts.Token);
            }
            StateHasChanged();
        }
        private void OnRowClicked(BillImportDetails item)
        {
            Nav.NavigateTo($"/product-serials?product-code={item.ProductCode}&product-name={item.ProductName}&detail-id={item.ID}");
        }
        private void OnBrowserBack()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            productDataService.ClearData();
            Nav.NavigateTo("/bill-imports");
        }
        private static string GetRowClass(int status)
        {
            if (status == 1) return "empty";
            if (status == 2) return "in-process";
            if (status == 3) return "finished";
            return "unknown";
        }
    }
}