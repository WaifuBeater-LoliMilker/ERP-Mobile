using ERP_Mobile.Models;
using ERP_Mobile.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Mobile.Services
{
    public interface IProductSerialDataService
    {
        public Task<int> SaveData(BillImportDetails billImportDetail);
        public BillImportSerialServiceData? GetData(int billImportDetailID);
        public void ClearData();
    }
    public class ProductSerialDataService : IProductSerialDataService
    {
        private List<BillImportSerialServiceData> _lst = [];
        private readonly IApiService _apiService;
        public ProductSerialDataService(IApiService apiService)
        {
            _apiService = apiService;
        }
        /// <summary>
        /// Gọi API để lấy serial và lưu lại trước khi chuyển trang
        /// </summary>
        /// <param name="billImportDetail">Chi tiết phiếu nhập</param>
        /// <returns>Trạng thái nhập serial: 1 = Chưa nhập, 2 = Nhập nhưng chưa đủ, 3 = Đã nhập đủ</returns>
        public async Task<int> SaveData(BillImportDetails billImportDetail)
        {
            var existing = GetData(billImportDetail.ID);
            if (existing != null) throw new ArgumentException("Chi tiết phiếu nhập đã tồn tại");
            var token = Preferences.Get("Token", "");
            _apiService.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _apiService.Client.GetAsync($"api/BillImportDetailSerialNumber/get-serialnumber?billImportDetailID={billImportDetail.ID}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Load dữ liệu thất bại.");
            }
            var json = await response.Content.ReadAsStringAsync();
            var serialsDTO = JsonConvert.DeserializeObject<ProductSerialsDTO>(json, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            var serials = serialsDTO!.data;
            _lst.Add(new BillImportSerialServiceData
            {
                details = billImportDetail,
                serials = serials
            });
            var qtyRequest = Convert.ToInt32(Math.Floor(billImportDetail.QtyRequest));
            if (serials.Count == 0 && serials.Count < qtyRequest) return 1;
            else if (serials.Count > 0 && serials.Count < qtyRequest) return 2;
            else if (serials.Count >= qtyRequest) return 3;
            else return 0;
        }
        public BillImportSerialServiceData? GetData(int billImportDetailID)
        {
            return _lst.FirstOrDefault(d => d.details.ID == billImportDetailID);
        }
        public void ClearData()
        {
            _lst.Clear();
        }
    }
}
