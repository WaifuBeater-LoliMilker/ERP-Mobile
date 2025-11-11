using ERP_Mobile.Models.DTO;
using ERP_Mobile.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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
    public partial class ProductSerials : IDisposable
    {
        [Parameter]
        [SupplyParameterFromQuery(Name = "detail-id")]
        public string? BillImportDetailID { get; set; }
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private IJSRuntime JS { get; set; } = default!;
        [Inject] private IApiService apiService { get; set; } = default!;
        [Inject] private IAlertService alertService { get; set; } = default!;
        [Inject] private IProductSerialDataService productDataService { get; set; } = default!;
        public string? ProductCode { get; set; } = "";
        public string? ProductName { get; set; } = "";
        private List<Models.ProductSerials> serialsData { get; set; } = [];
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var data = productDataService.GetData(Convert.ToInt32(BillImportDetailID));
                ProductCode = data?.details.ProductCode;
                ProductName = data?.details.ProductName;
                serialsData = data?.serials ?? [];
                if (data != null && serialsData.Count < data.details.QtyRequest)
                {
                    int missing = (int)data.details.QtyRequest - serialsData.Count;
                    int maxSTT = serialsData.Count == 0 ? 0 : serialsData.Max(d => d.STT);
                    for (int i = 0; i < missing; i++)
                    {
                        serialsData.Add(new Models.ProductSerials
                        {
                            ID = 0,
                            BillImportDetailID = data.details.ID,
                            STT = maxSTT + i + 1,
                            SerialNumber = "",
                            SerialNumberRTC = "",
                        });
                    }
                }
                StateHasChanged();
            }
            return Task.CompletedTask;
        }
        private async Task OnEnterKey(KeyboardEventArgs e, int currentIndex, string column)
        {
            if (e.Code == "Enter" || e.Key == "Enter" || e.Code == "NumpadEnter")
            {
                int nextIndex = currentIndex + 1;

                if (nextIndex < serialsData.Count)
                {
                    string nextId = $"{column}-{nextIndex}";
                    await JS.InvokeVoidAsync("setFocusById", nextId);
                }
                try
                {
                    await Task.Run(async () =>
                    {
                        var token = Preferences.Get("Token", "");
                        apiService.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        var serialized = JsonConvert.SerializeObject(new[] { serialsData[currentIndex] });
                        var jsonContent = new StringContent(serialized,
                        Encoding.UTF8,
                        "application/json");
                        var response = await apiService.Client.PostAsync($"api/billimportdetailserialnumber/save-data", jsonContent);
                        //if (!response.IsSuccessStatusCode)
                        //{
                        //    throw new Exception("Thao tác thất bại");
                        //}
                        var json = await response.Content.ReadAsStringAsync();
                        var productSerialsDTO = JsonConvert.DeserializeObject<ProductSerialsDTO>(json);
                        if (productSerialsDTO?.status != 1) throw new Exception(productSerialsDTO?.message ?? "Thao tác thất bại");
                    });
                }
                catch(Exception ex)
                {
                    await alertService.ShowAsync("Thông báo", ex.Message, "OK");
                }
            }
        }
        private async Task OnBrowserBack()
        {
            await JS.InvokeVoidAsync("history.back");
        }
        public void Dispose()
        {
            serialsData.Clear();
            productDataService.ClearData();
            GC.SuppressFinalize(this);
        }
    }
}
