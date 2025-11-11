using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Mobile.Models
{
    public class BillImports
    {
        public int TotalPage { get; set; } = 0;
        public int RowNum { get; set; } = 0;
        public int ID { get; set; } = 0;
        public string BillImportCode { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public string? WarehouseName { get; set; }
        public string? DateStatus { get; set; }
        public string? Deliver { get; set; }
        public string? Reciver { get; set; }
        public string? Suplier { get; set; }
        public bool? BillType { get; set; }
        public string? KhoType { get; set; }
        public string? GroupID { get; set; }
        public int? SupplierID { get; set; }
        public int? DeliverID { get; set; }
        public int? ReciverID { get; set; }
        public int? KhoTypeID { get; set; }
        public DateTime? CreatDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public int? UnApprove { get; set; }
        public bool? PTNB { get; set; }
        public int? WarehouseID { get; set; }
        public int? BillTypeNew { get; set; }
        public int? BillDocumentImportType { get; set; }
        public DateTime DateRequestImport { get; set; }
        public int? RulePayID { get; set; }
        public bool? IsDeleted { get; set; }
        public int? BillExportID { get; set; }
        public bool? StatusDocumentImport { get; set; }
        public string? BillTypeText { get; set; }
        public string? Code { get; set; }
        public string? DepartmentName { get; set; }
        public string? Overdue { get; set; }
        public string? IsSuccessText { get; set; }
        public string? DoccumentReceiver { get; set; }
        public int? DoccumentReceiverID { get; set; }
        public string? CurrencyList { get; set; }
        public decimal? VAT { get; set; }
        public string? PONCCCodeList { get; set; }
    }
}