using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Mobile.Models
{
    public class ProductSerials
    {
        public int ID { get; set; } = 0;
        public int BillImportDetailID { get; set; } = 0;
        public int STT { get; set; } = 0;
        public string SerialNumber { get; set; } = string.Empty;
        public string SerialNumberRTC { get; set; } = string.Empty;
    }
}
