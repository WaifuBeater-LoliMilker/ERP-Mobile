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
    public partial class BillImport
    {
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private IJSRuntime JS { get; set; } = default!;
        [Inject] private IApiService apiService { get; set; } = default!;
        [Inject] private IAlertService alertService { get; set; } = default!;
        private string filterText { get; set; } = "";
        private DateTime dateStart { get; set; } = DateTime.Now.AddDays(-14).Date;
        private DateTime dateEnd { get; set; } = DateTime.Now;
        private List<Models.BillImport> billImport = [];
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
            var payload = new
            {
                KhoType = "4",
                Status = 4,
                DateStart = dateStart,
                DateEnd = dateEnd,
                FilterText = filterText,
                PageNumber = 1,
                PageSize = 1000000,
                WarehouseCode = "HN",
                checkedAll = true
            };
            var serialized = JsonConvert.SerializeObject(payload);
            var jsonContent = new StringContent(serialized,
                Encoding.UTF8,
                "application/json");
            var token = Preferences.Get("Token", "");
            apiService.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await apiService.Client.PostAsync($"api/BillImport", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Load dữ liệu thất bại.");
            }
            var json = await response.Content.ReadAsStringAsync();
            var billImportDTO = JsonConvert.DeserializeObject<BillImportDTO>(json);
            billImport = billImportDTO?.data ?? [];
            StateHasChanged();
        }
    }
}
