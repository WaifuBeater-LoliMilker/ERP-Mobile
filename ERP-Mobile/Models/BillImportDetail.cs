using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Mobile.Models
{
    public class BillImportDetails
    {
        public string ProductCode { get; set; }
        public string ProductNewCode { get; set; }
        public string ProductName { get; set; }
        public int ProductGroupID { get; set; }
        public int ID { get; set; }
        public int BillImportID { get; set; }
        public int ProductID { get; set; }
        public decimal Qty { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public string? SomeBill { get; set; }
        public string? Note { get; set; }
        public int STT { get; set; }
        public decimal TotalQty { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int ProjectID { get; set; }
        public int PONCCDetailID { get; set; }
        public string SerialNumber { get; set; }
        public string CodeMaPhieuMuon { get; set; }
        public int BillExportDetailID { get; set; }
        public int ProjectPartListID { get; set; }
        public bool IsKeepProject { get; set; }
        public decimal QtyRequest { get; set; }
        public string? BillCodePO { get; set; }
        public bool? ReturnedStatus { get; set; }
        public DateTime? DateSomeBill { get; set; }
        public bool IsDeleted { get; set; }
        public string? UnitName { get; set; }
        public decimal DPO { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal TaxReduction { get; set; }
        public decimal COFormE { get; set; }
        public bool IsNotKeep { get; set; }
        public string ItemType { get; set; }
        public DateTime? CreatDate { get; set; }
        public string Suplier { get; set; }
        public int DeliverID { get; set; }
        public string Deliver { get; set; }
        public string Reciver { get; set; }
        public int ReciverID { get; set; }
        public string Unit { get; set; }
        public decimal Qty1 { get; set; }
        public string Maker { get; set; }
        public string ProductGroupName { get; set; }
        public string CodeNCC { get; set; }
        public string NameNCC { get; set; }
        public string FullName { get; set; }
        public string WarehouseName { get; set; }
        public string CodeMaPhieuMuon1 { get; set; }
        public string ProductFullName { get; set; }
        public string ProjectCodeText { get; set; }
        public string ProjectNameText { get; set; }
        public string ProjectCodeExport { get; set; }
        public string SomeBill1 { get; set; }
        public int IdMapping { get; set; }
        public string RulePayName { get; set; }
        public string CustomerFullName { get; set; }
        public int ProjectPartListQuantity { get; set; }
        public DateTime? DateSomeBill1 { get; set; }
        public int? BillImportQCID { get; set; }
        public string StatusQCText { get; set; }
        public string Overdue { get; set; }
        public DateTime? DealineQC { get; set; }
        public bool IsStock { get; set; }
        public string UnitName1 { get; set; }
        public string POKHDetailQuantity { get; set; }
        public string POKHDetailID { get; set; }
        public decimal QuantityRequestBuy { get; set; }
        public int CustomerID { get; set; }
        public string ChosenInventoryProject { get; set; }
        public string ProductCodeExport { get; set; }
        public string PONumber { get; set; }
        public int ChildID { get; set; }
        public string InventoryProjectID { get; set; }
    }

}
